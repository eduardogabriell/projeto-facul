using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projeto_facul.Models;

namespace projeto_facul.Controllers
{
    public class GastosController : Controller
    {
        private readonly AppDbContext _context;

        public GastosController(AppDbContext context)
        {
            _context = context;
        }

        private const int ItensPorPagina = 7;

        private static readonly CultureInfo CulturaBr = new("pt-BR");

        private static readonly string[] NomesMeses =
        {
            "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
            "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
        };

        // GET: Gastos
        public async Task<IActionResult> Index(string? busca, string? contexto, string? categoria, string? mes, int pagina = 1)
        {
            var modelo = new GastosIndexViewModel
            {
                Busca = busca,
                Contexto = string.IsNullOrEmpty(contexto) ? "Pessoal" : contexto,
                Categoria = string.IsNullOrEmpty(categoria) ? "Todas" : categoria,
                Mes = string.IsNullOrEmpty(mes) ? DateTime.Today.ToString("yyyy-MM") : mes
            };

            var consulta = Filtrar(modelo);

            modelo.TotalItens = await consulta.CountAsync();
            modelo.TotalPaginas = Math.Max(1, (int)Math.Ceiling(modelo.TotalItens / (double)ItensPorPagina));
            modelo.Pagina = Math.Clamp(pagina, 1, modelo.TotalPaginas);

            modelo.Itens = await consulta
                .OrderByDescending(g => g.Data)
                .Skip((modelo.Pagina - 1) * ItensPorPagina)
                .Take(ItensPorPagina)
                .ToListAsync();

            modelo.Inicio = modelo.TotalItens == 0 ? 0 : (modelo.Pagina - 1) * ItensPorPagina + 1;
            modelo.Fim = modelo.Inicio == 0 ? 0 : modelo.Inicio + modelo.Itens.Count - 1;

            // Meses que aparecem no filtro: os que têm gastos + o mês atual
            var meses = await _context.Gastos
                .Select(g => new { g.Data.Year, g.Data.Month })
                .Distinct()
                .ToListAsync();
            var datas = meses.Select(m => new DateTime(m.Year, m.Month, 1)).ToList();
            datas.Add(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1));
            modelo.Meses = datas.Distinct()
                .OrderByDescending(d => d)
                .Select(d => (d.ToString("yyyy-MM"), $"{NomesMeses[d.Month - 1]} / {d.Year}"))
                .ToList();

            return View(modelo);
        }

        // GET: Gastos/Exportar
        public async Task<IActionResult> Exportar(string? busca, string? contexto, string? categoria, string? mes)
        {
            var filtros = new GastosIndexViewModel
            {
                Busca = busca,
                Contexto = string.IsNullOrEmpty(contexto) ? "Pessoal" : contexto,
                Categoria = string.IsNullOrEmpty(categoria) ? "Todas" : categoria,
                Mes = string.IsNullOrEmpty(mes) ? DateTime.Today.ToString("yyyy-MM") : mes
            };

            var gastos = await Filtrar(filtros).OrderByDescending(g => g.Data).ToListAsync();

            // CSV com ";" e vírgula decimal, para abrir direto no Excel em português
            var csv = new StringBuilder();
            csv.AppendLine("Item / Descrição;Estabelecimento;Categoria;Contexto;Data;Valor");
            foreach (var g in gastos)
            {
                csv.AppendLine(string.Join(";",
                    Csv(g.Descricao), Csv(g.Estabelecimento), Csv(g.Categoria), Csv(g.Contexto),
                    g.Data.ToString("dd/MM/yyyy HH:mm", CulturaBr),
                    g.Valor.ToString("0.00", CulturaBr)));
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(csv.ToString())).ToArray();
            return File(bytes, "text/csv", $"gastos-{DateTime.Now:yyyyMMdd-HHmm}.csv");
        }

        private IQueryable<Gasto> Filtrar(GastosIndexViewModel filtros)
        {
            var consulta = _context.Gastos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtros.Busca))
            {
                var termo = filtros.Busca.Trim();
                consulta = consulta.Where(g => g.Descricao.Contains(termo)
                    || g.Estabelecimento.Contains(termo)
                    || g.Categoria.Contains(termo));
            }

            if (filtros.Contexto != "Todos")
            {
                consulta = consulta.Where(g => g.Contexto == filtros.Contexto);
            }

            if (filtros.Categoria != "Todas")
            {
                consulta = consulta.Where(g => g.Categoria == filtros.Categoria);
            }

            if (filtros.Mes != "todos" && DateTime.TryParseExact(filtros.Mes, "yyyy-MM",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var inicioMes))
            {
                var fimMes = inicioMes.AddMonths(1);
                consulta = consulta.Where(g => g.Data >= inicioMes && g.Data < fimMes);
            }

            return consulta;
        }

        private static string Csv(string texto) =>
            texto.Contains(';') || texto.Contains('"') ? $"\"{texto.Replace("\"", "\"\"")}\"" : texto;

        // GET: Gastos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // GET: Gastos/Create
        public IActionResult Create()
        {
            var agora = DateTime.Now;
            return View(new Gasto { Data = new DateTime(agora.Year, agora.Month, agora.Day, agora.Hour, agora.Minute, 0) });
        }

        // POST: Gastos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Estabelecimento,Categoria,Contexto,Valor,Data")] Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gasto);
        }

        // GET: Gastos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }
            return View(gasto);
        }

        // POST: Gastos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Estabelecimento,Categoria,Contexto,Valor,Data")] Gasto gasto)
        {
            if (id != gasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoExists(gasto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(gasto);
        }

        // GET: Gastos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gasto = await _context.Gastos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gasto == null)
            {
                return NotFound();
            }

            return View(gasto);
        }

        // POST: Gastos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto != null)
            {
                _context.Gastos.Remove(gasto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Html;

namespace projeto_facul.Models
{
    // Ícones SVG usados nas telas de Gastos (traço simples, herdam a cor do texto)
    public static class Icones
    {
        private static HtmlString Svg(string conteudo, int tamanho = 18) =>
            new HtmlString($"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{tamanho}\" height=\"{tamanho}\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" aria-hidden=\"true\">{conteudo}</svg>");

        public static HtmlString Logo => Svg("<path d=\"M12 4 20 12 12 20 4 12z\"/><path d=\"M12 9.5 14.5 12 12 14.5 9.5 12z\"/>", 20);
        public static HtmlString Casa => Svg("<path d=\"M3 10.5 12 3l9 7.5V20a1 1 0 0 1-1 1h-5v-6H9v6H4a1 1 0 0 1-1-1z\"/>");
        public static HtmlString Lista => Svg("<circle cx=\"12\" cy=\"12\" r=\"9\"/><path d=\"m15 9-6 6M9 9l6 6\"/>");
        public static HtmlString Pasta => Svg("<path d=\"M3 18V6a2 2 0 0 1 2-2h4l2 2h7a2 2 0 0 1 2 2v2\"/><path d=\"M3 18l2.6-6.5A2 2 0 0 1 7.5 10H21l-2.7 7.1a2 2 0 0 1-1.9 1.4H3z\"/>");
        public static HtmlString Escanear => Svg("<path d=\"M3 7V5a2 2 0 0 1 2-2h2M17 3h2a2 2 0 0 1 2 2v2M21 17v2a2 2 0 0 1-2 2h-2M7 21H5a2 2 0 0 1-2-2v-2\"/><path d=\"M8 14.5s1.5 2 4 2 4-2 4-2M9 9.5h.01M15 9.5h.01\"/>");
        public static HtmlString Engrenagem => Svg("<path d=\"M12.22 2h-.44a2 2 0 0 0-2 2v.18a2 2 0 0 1-1 1.73l-.43.25a2 2 0 0 1-2 0l-.15-.08a2 2 0 0 0-2.73.73l-.22.38a2 2 0 0 0 .73 2.73l.15.1a2 2 0 0 1 1 1.72v.51a2 2 0 0 1-1 1.74l-.15.09a2 2 0 0 0-.73 2.73l.22.38a2 2 0 0 0 2.73.73l.15-.08a2 2 0 0 1 2 0l.43.25a2 2 0 0 1 1 1.73V20a2 2 0 0 0 2 2h.44a2 2 0 0 0 2-2v-.18a2 2 0 0 1 1-1.73l.43-.25a2 2 0 0 1 2 0l.15.08a2 2 0 0 0 2.73-.73l.22-.39a2 2 0 0 0-.73-2.73l-.15-.08a2 2 0 0 1-1-1.74v-.5a2 2 0 0 1 1-1.74l.15-.09a2 2 0 0 0 .73-2.73l-.22-.38a2 2 0 0 0-2.73-.73l-.15.08a2 2 0 0 1-2 0l-.43-.25a2 2 0 0 1-1-1.73V4a2 2 0 0 0-2-2z\"/><circle cx=\"12\" cy=\"12\" r=\"3\"/>");
        public static HtmlString Busca => Svg("<circle cx=\"11\" cy=\"11\" r=\"7\"/><path d=\"m20 20-3.5-3.5M11 8.5v5M8.5 11h5\"/>", 16);
        public static HtmlString SetaBaixo => Svg("<path d=\"m6 9 6 6 6-6\"/>", 14);
        public static HtmlString Calendario => Svg("<rect x=\"3\" y=\"4.5\" width=\"18\" height=\"17\" rx=\"2\"/><path d=\"M16 2.5v4M8 2.5v4M3 10h18\"/>", 15);
        public static HtmlString Exportar => Svg("<path d=\"M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4\"/><path d=\"m17 8-5-5-5 5M12 3v12\"/>", 16);
        public static HtmlString Mais => Svg("<path d=\"M12 5v14M5 12h14\"/>", 16);
        public static HtmlString Item => Svg("<path d=\"M4 7h9M4 17h9M4 12h13m-3-3 3 3-3 3\"/>", 15);
        public static HtmlString Editar => Svg("<path d=\"M12 3H5a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7\"/><path d=\"M18.4 2.6a2.1 2.1 0 0 1 3 3L12 15l-4 1 1-4z\"/>", 14);
        public static HtmlString Lixeira => Svg("<path d=\"M3 6h18M8 6V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2M19 6l-1 14a2 2 0 0 1-2 2H8a2 2 0 0 1-2-2L5 6M10 11v6M14 11v6\"/>", 14);
    }
}

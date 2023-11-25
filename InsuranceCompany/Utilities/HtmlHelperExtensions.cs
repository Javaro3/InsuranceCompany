using InsuranceCompany.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsuranceCompany.Utilities {
    public static class HtmlHelperExtensions {
        public static IHtmlContent AddIcon(this IHtmlHelper htmlHelper, int width, int height) {
            var svg = $@"
                <svg xmlns='http://www.w3.org/2000/svg' width='{width}' height='{height}' fill='currentColor' class='bi bi-plus-circle' viewBox='0 0 16 16'>
                    <path d='M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16' />
                    <path d='M8 4a.5.5 0 0 1 .5.5v3h3a.5.5 0 0 1 0 1h-3v3a.5.5 0 0 1-1 0v-3h-3a.5.5 0 0 1 0-1h3v-3A.5.5 0 0 1 8 4' />
                </svg>";

            return new HtmlString(svg);
        }

        public static IHtmlContent FilterIcon(this IHtmlHelper htmlHelper, int width, int height) {
            var svg = $@"
                <svg xmlns='http://www.w3.org/2000/svg' width='{width}' height='{height}' fill='currentColor' class='bi bi-funnel-fill' viewBox='0 0 16 16'>
                    <path d=""M1.5 1.5A.5.5 0 0 1 2 1h12a.5.5 0 0 1 .5.5v2a.5.5 0 0 1-.128.334L10 8.692V13.5a.5.5 0 0 1-.342.474l-3 1A.5.5 0 0 1 6 14.5V8.692L1.628 3.834A.5.5 0 0 1 1.5 3.5z"" />
                </svg>";

            return new HtmlString(svg);
        }

        public static IHtmlContent DeleteIcon(this IHtmlHelper htmlHelper, int width, int height) {
            var svg = $@"
                <svg xmlns='http://www.w3.org/2000/svg' width='{width}' height='{height}' fill='currentColor' class='bi bi-trash3' viewBox='0 0 16 16'>
                    <path d='M6.5 1h3a.5.5 0 0 1 .5.5v1H6v-1a.5.5 0 0 1 .5-.5M11 2.5v-1A1.5 1.5 0 0 0 9.5 0h-3A1.5 1.5 0 0 0 5 1.5v1H2.506a.58.58 0 0 0-.01 0H1.5a.5.5 0 0 0 0 1h.538l.853 10.66A2 2 0 0 0 4.885 16h6.23a2 2 0 0 0 1.994-1.84l.853-10.66h.538a.5.5 0 0 0 0-1h-.995a.59.59 0 0 0-.01 0zm1.958 1-.846 10.58a1 1 0 0 1-.997.92h-6.23a1 1 0 0 1-.997-.92L3.042 3.5zm-7.487 1a.5.5 0 0 1 .528.47l.5 8.5a.5.5 0 0 1-.998.06L5 5.03a.5.5 0 0 1 .47-.53Zm5.058 0a.5.5 0 0 1 .47.53l-.5 8.5a.5.5 0 1 1-.998-.06l.5-8.5a.5.5 0 0 1 .528-.47ZM8 4.5a.5.5 0 0 1 .5.5v8.5a.5.5 0 0 1-1 0V5a.5.5 0 0 1 .5-.5' />
                </svg>";

            return new HtmlString(svg);
        }

        public static IHtmlContent InfoIcon(this IHtmlHelper htmlHelper, int width, int height) {
            var svg = $@"
                <svg xmlns='http://www.w3.org/2000/svg' width='{width}' height='{height}' fill='currentColor' class='bi bi-info-circle' viewBox='0 0 16 16'>
                    <path d='M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16' />
                    <path d='m8.93 6.588-2.29.287-.082.38.45.083c.294.07.352.176.288.469l-.738 3.468c-.194.897.105 1.319.808 1.319.545 0 1.178-.252 1.465-.598l.088-.416c-.2.176-.492.246-.686.246-.275 0-.375-.193-.304-.533zM9 4.5a1 1 0 1 1-2 0 1 1 0 0 1 2 0' />
                </svg>";

            return new HtmlString(svg);
        }

        public static IHtmlContent EditIcon(this IHtmlHelper htmlHelper, int width, int height) {
            var svg = $@"
                <svg xmlns='http://www.w3.org/2000/svg' width='{width}' height='{height}' fill='currentColor' class='bi bi-pencil' viewBox='0 0 16 16'>
                    <path d='M12.146.146a.5.5 0 0 1 .708 0l3 3a.5.5 0 0 1 0 .708l-10 10a.5.5 0 0 1-.168.11l-5 2a.5.5 0 0 1-.65-.65l2-5a.5.5 0 0 1 .11-.168l10-10zM11.207 2.5 13.5 4.793 14.793 3.5 12.5 1.207zm1.586 3L10.5 3.207 4 9.707V10h.5a.5.5 0 0 1 .5.5v.5h.5a.5.5 0 0 1 .5.5v.5h.293zm-9.761 5.175-.106.106-1.528 3.821 3.821-1.528.106-.106A.5.5 0 0 1 5 12.5V12h-.5a.5.5 0 0 1-.5-.5V11h-.5a.5.5 0 0 1-.468-.325z' />
                </svg>";

            return new HtmlString(svg);
        }

        public static IHtmlContent InsuranceTypesHorizontalScrollView(this IHtmlHelper htmlHelper, IEnumerable<InsuranceType> insuranceTypes) {
            var result = new HtmlContentBuilder();

            foreach (var insuranceType in insuranceTypes) {
                var card = new TagBuilder("div");
                card.AddCssClass("card");
                card.Attributes.Add("style", "width: 40rem; height: 20rem;");

                var cardBody = new TagBuilder("div");
                cardBody.AddCssClass("card-body");

                var infoBlock1 = new TagBuilder("div");
                infoBlock1.AddCssClass("info-block");
                infoBlock1.Attributes.Add("data-toggle", "tooltip");
                infoBlock1.Attributes.Add("data-placement", "top");
                infoBlock1.Attributes.Add("title", "Наведите для анимации");
                infoBlock1.InnerHtml.AppendHtml($"<h5 class='card-title'>{insuranceType.Name}</h5>");

                var infoBlock2 = new TagBuilder("div");
                infoBlock2.AddCssClass("info-block");
                infoBlock2.Attributes.Add("style", "height:10rem;");
                infoBlock2.Attributes.Add("data-toggle", "tooltip");
                infoBlock2.Attributes.Add("data-placement", "top");
                infoBlock2.Attributes.Add("title", "Наведите для анимации");
                infoBlock2.InnerHtml.AppendHtml("<h5 class='card-title'>Описание</h5>");
                infoBlock2.InnerHtml.AppendHtml($"<p class='card-text'>{insuranceType.Description}</p>");

                cardBody.InnerHtml.AppendHtml(infoBlock1);
                cardBody.InnerHtml.AppendHtml(infoBlock2);

                card.InnerHtml.AppendHtml(cardBody);

                result.AppendHtml(card);
            }

            return result;
        }

        public static IHtmlContent GenerateMenuForRole(this IHtmlHelper htmlHelper, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) {
            if (signInManager.IsSignedIn(htmlHelper.ViewContext.HttpContext.User)) {
                var user = userManager.GetUserAsync(htmlHelper.ViewContext.HttpContext.User).Result;
                var role = userManager.GetRolesAsync(user).Result[0];

                return role == "Клиент" ? GenerateClientMenu() : GenerateInsuranceAgentMenu();
            }
            return GenerateDefaultMenu();
        }

        private static IHtmlContent GenerateClientMenu() {
            var result = new HtmlContentBuilder();

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"Home\" asp-action=\"Index\">Полисы</a>");
            result.AppendHtml("</li>");

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"Home\" asp-action=\"Index\">Страховые случаи</a>");
            result.AppendHtml("</li>");

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"Home\" asp-action=\"Index\">Подтверждающие документы</a>");
            result.AppendHtml("</li>");

            return result;
        }

        private static IHtmlContent GenerateInsuranceAgentMenu() {
            var result = new HtmlContentBuilder();

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"InsuranceTypes\" asp-action=\"Index\">Типы страховок</a>");
            result.AppendHtml("</li>");

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"Home\" asp-action=\"Index\">Полисы</a>");
            result.AppendHtml("</li>");

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"Home\" asp-action=\"Index\">Контракты</a>");
            result.AppendHtml("</li>");

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"Home\" asp-action=\"Index\">Клиенты</a>");
            result.AppendHtml("</li>");

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"Home\" asp-action=\"Index\">Страховые агенты</a>");
            result.AppendHtml("</li>");

            return result;
        }

        private static IHtmlContent GenerateDefaultMenu() {
            var result = new HtmlContentBuilder();

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"Home\" asp-action=\"Index\">Главная страница</a>");
            result.AppendHtml("</li>");

            return result;
        }
    }
}
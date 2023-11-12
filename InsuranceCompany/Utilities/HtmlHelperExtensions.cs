using InsuranceCompany.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InsuranceCompany.Utilities {
    public static class HtmlHelperExtensions {
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
            var result = new HtmlContentBuilder();

            if (signInManager.IsSignedIn(htmlHelper.ViewContext.HttpContext.User)) {
                var user = userManager.GetUserAsync(htmlHelper.ViewContext.HttpContext.User).Result;
                var roles = userManager.GetRolesAsync(user).Result;

                if (roles.Count > 0) {
                    var role = roles[0];

                    if (role == "Клиент") {
                        result.AppendHtml(GenerateClientMenu());
                    }
                    else {
                        result.AppendHtml(GenerateAdminMenu());
                    }
                }
            }
            else {
                result.AppendHtml(GenerateDefaultMenu());
            }

            return result;
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

        private static IHtmlContent GenerateAdminMenu() {
            var result = new HtmlContentBuilder();

            result.AppendHtml("<li class=\"nav-item\">");
            result.AppendHtml("<a class=\"nav-link text-dark\" asp-area=\"\" asp-controller=\"Home\" asp-action=\"Index\">Типы страховок</a>");
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
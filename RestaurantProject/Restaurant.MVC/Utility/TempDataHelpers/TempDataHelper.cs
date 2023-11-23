using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Restaurant.MVC.Utility.TempDataHelpers
{
    public static class TempDataHelper
    {
        public static void SetSuccessMessage(this ITempDataDictionary tempData)
        {
            tempData["Message"] = "İşlem Başarılı";
        }

        public static void SetErrorMessage(this ITempDataDictionary tempData)
        {
            tempData["ErrorMessage"] = "Eksiklikleri tamamlayınız!";
        }
        public static void NotFoundId(this ITempDataDictionary tempData)
        {
            tempData["ErrorMessage"] = "Id No bulunamadı";
        }
        public static void NoAuthorizationMessage(this ITempDataDictionary tempData)
        {
            tempData["ErrorMessage"] = "Bu Sayfa için yetkiniz yok";
        }
    }
}

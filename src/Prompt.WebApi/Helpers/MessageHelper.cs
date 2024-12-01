﻿namespace Prompt.WebApi.Helpers
{
    public static class MessageHelper
    {
        public static string GeneralValidationErrorMessage => "Bir veya daha fazla validasyon hatası oluştu.";
        public static string GeneralErrorMessage => "Bir veya daha fazla validasyon hatası oluştu.";

        public static string GetApiSuccessCreatedMessage(string entityName) => $"{entityName} başarıyla oluşturuldu.";
        public static string GetApiSuccessUpdatedMessage(string entityName) => $"{entityName} başarıyla güncellendi.";
        public static string GetApiSuccessDeletedMessage(string entityName) => $"{entityName} başarıyla silindi.";

    }
}

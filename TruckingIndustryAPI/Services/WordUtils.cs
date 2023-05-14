using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

using System.Text.RegularExpressions;

namespace TruckingIndustryAPI.Services
{
    public static class WordUtils
    {
        public static void ReplaceText(string filePath, Dictionary<string, string> dictionary)
        {
            using (var doc = WordprocessingDocument.Open(filePath, true))
            {
                var body = doc.MainDocumentPart.Document.Body;
                var regex = new Regex(@"\{([^\{\}]+)\}");

                foreach (var text in body.Descendants<Text>())
                {
                    text.Text = regex.Replace(text.Text, match =>
                    {
                        var key = match.Groups[1].Value;
                        if (dictionary.TryGetValue(key, out string value))
                        {
                            return value;
                        }
                        else
                        {
                            return match.Value;
                        }
                    });
                }

                doc.Save();
            }
        }



    }
}

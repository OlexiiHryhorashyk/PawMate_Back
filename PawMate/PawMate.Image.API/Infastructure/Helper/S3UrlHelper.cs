namespace PawMate.Image.API.Infastructure.Helper
{
    public static class S3UrlHelper
    {
        public static string GetFileNameFromUrl(string url)
        {
            return Path.GetFileName(url);
        }

        public static string GetPathFromUrl(string url)
        {           
            int startPosition = 0;
            int count = 0;
            for(int i = 0; i < url.Length; i++)
            {
                if (url[i] == '/') count++;
                if(count == 3)
                {
                    startPosition = i+1;
                    break;
                }
            }

            return url.Substring(startPosition);
        }

        public static string GetPath(string id, string userName, string imageName)
        {
            return $"{id}-{userName}/{imageName}";
        }
    }
}

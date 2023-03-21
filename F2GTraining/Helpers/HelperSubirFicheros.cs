namespace F2GTraining.Helpers
{
    public class HelperSubirFicheros
    {
        private HelperRutasProvider helperRuta;
        public HelperSubirFicheros(HelperRutasProvider pathProvider)
        {
            this.helperRuta = pathProvider;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string nombre)
        {
            string fileName = nombre + ".png";
            string path = this.helperRuta.MapPath(fileName);

            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string[] separatedRoute = path.Split("\\");

            string routeFileRoot = "~/" 
                + separatedRoute[separatedRoute.Count() - 2] + "/" 
                + separatedRoute[separatedRoute.Count() - 1];

            return fileName;
        }
    }
}

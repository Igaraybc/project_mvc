using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

[Area("Admin")]
[Authorize("Admin")]
public class AdminImagesController: Controller
{
    private readonly ConfigurationImages _configurationImages;
    private readonly IWebHostEnvironment _hostingEnviroment;

    public AdminImagesController(IOptions<ConfigurationImages> configurationImages, IWebHostEnvironment hostingEnviroment)
    {
        _configurationImages = configurationImages.Value;
        _hostingEnviroment = hostingEnviroment;
    }

    public IActionResult Index(){
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadFiles(List<IFormFile> files){
        if(files == null || files.Count == 0){
            ViewData["Error"] = "Error: Arquivo não selecionado.";
            return View(ViewData);
        }

        if(files.Count > 10){
            ViewData["Error"] = "Error: Quantidade de arquivos excedeu o limite";
            return View(ViewData);
        }

        long size = files.Sum(f => f.Length);
        var filePathsName = new List<string>();
        
        var filePath = Path.Combine(_hostingEnviroment.WebRootPath, _configurationImages.ProductImagesPath);

        foreach(var formFile in files){
            if(formFile.FileName.Contains(".jpg") || formFile.FileName.Contains(".png") || formFile.FileName.Contains(".gif")){
                var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);

                filePathsName.Add(fileNameWithPath);

                using(var stream = new FileStream(fileNameWithPath, FileMode.Create)){
                    await formFile.CopyToAsync(stream); 
                }
            }
        }

        ViewData["Result"] = $"{files.Count} arquivos foram enviados ao servidor, com tamanho total de: {size} bytes";
        ViewBag.Files = filePathsName;

        return View(ViewData);
    }

    public IActionResult GetImages(){
        FileManagerModel model = new();

        var filePath = Path.Combine(_hostingEnviroment.WebRootPath, _configurationImages.ProductImagesPath);

        DirectoryInfo dir = new DirectoryInfo(filePath);

        FileInfo[] files = dir.GetFiles();

        model.PathImagesProduct = _configurationImages.ProductImagesPath;

        if(files.Length == 0){
            ViewData["Error"] = $"Nenhum arquivo encontrado na pasta {filePath}";
        }

        model.Files = files;
        return View(model);
    }

    public IActionResult DeleteFile(string fname){
        string _imageDelete = Path.Combine(_hostingEnviroment.WebRootPath, _configurationImages.ProductImagesPath + "\\", fname);

        if((System.IO.File.Exists(_imageDelete))){
            System.IO.File.Delete(_imageDelete);

            ViewData["Deleted"] = $"Arquivo(s) {_imageDelete} deletada com sucesso";
        }

        return View("Index");
    }
}
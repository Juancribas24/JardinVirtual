using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleFileBrowser;
using System.IO;

public class ImageUploader : MonoBehaviour
{
    public string flaskUrl = "http://127.0.0.1:5000/stylize-image"; // Endpoint Flask
    public RawImage canvasImage; // Donde se mostrará la imagen estilizada
    private string contentImagePath; // Ruta de la imagen de contenido seleccionada
    public Texture2D[] styleImages; // Arreglo de imágenes de estilo (arrastradas al inspector)
    private Texture2D selectedStyleImage; // Imagen de estilo seleccionada

    // Método para abrir el explorador de archivos y seleccionar la imagen de contenido
    public void OpenFileExplorer()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".png", ".jpg", ".jpeg"));
        FileBrowser.SetDefaultFilter(".png");

        FileBrowser.ShowLoadDialog(
            (paths) => { OnFileSelected(paths[0]); }, // Callback cuando se selecciona un archivo
            () => { Debug.Log("Selección de archivo cancelada."); }, // Callback si se cancela
            FileBrowser.PickMode.Files);
    }

    // Callback para manejar la imagen seleccionada
    private void OnFileSelected(string path)
    {
        if (!System.IO.File.Exists(path))
        {
            Debug.LogError($"El archivo seleccionado no existe: {path}");
            return;
        }

        // Guardar la ruta de la imagen seleccionada
        contentImagePath = path;
        Debug.Log($"Imagen seleccionada: {contentImagePath}");
    }

    // Método para seleccionar el estilo a partir del índice del arreglo
    public void SelectStyle(int index)
    {
        if (index < 0 || index >= styleImages.Length)
        {
            Debug.LogError($"Índice de estilo inválido: {index}");
            return;
        }

        selectedStyleImage = styleImages[index];
        Debug.Log($"Estilo seleccionado: {selectedStyleImage.name}");
    }

    // Método para enviar las imágenes al servidor Flask
    public void UploadImages()
    {
        if (string.IsNullOrEmpty(contentImagePath))
        {
            Debug.LogError("No se ha seleccionado ninguna imagen de contenido.");
            return;
        }

        if (selectedStyleImage == null)
        {
            Debug.LogError("No se ha seleccionado ninguna imagen de estilo.");
            return;
        }

        StartCoroutine(UploadImagesCoroutine());
    }

    // Corrutina para cargar las imágenes y enviarlas al servidor
    private IEnumerator UploadImagesCoroutine()
    {
        Debug.Log("Preparando imágenes para enviar...");

        // Verificar rutas y cargar el archivo de contenido
        if (!System.IO.File.Exists(contentImagePath))
        {
            Debug.LogError($"El archivo de contenido no existe: {contentImagePath}");
            yield break;
        }

        byte[] contentImageData = System.IO.File.ReadAllBytes(contentImagePath);
        byte[] styleImageData = selectedStyleImage.EncodeToJPG(); // Convertir Texture2D a PNG

        Debug.Log("Archivos cargados correctamente. Creando formulario...");

        // Crear formulario para enviar los datos
        WWWForm form = new WWWForm();
        form.AddBinaryData("content_image", contentImageData, "content_image.png", "image/png");
        form.AddBinaryData("style_image", styleImageData, selectedStyleImage.name + ".png", "image/png");

        Debug.Log("Enviando solicitud al servidor...");

        // Enviar solicitud al servidor Flask
        using (UnityWebRequest request = UnityWebRequest.Post(flaskUrl, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Imagen estilizada recibida.");

                // Procesar la imagen recibida
                byte[] resultImageData = request.downloadHandler.data;
                Texture2D resultTexture = new Texture2D(2, 2);
                resultTexture.LoadImage(resultImageData);

                // Asignar la imagen al Canvas
                if (canvasImage != null)
                {
                    canvasImage.texture = resultTexture;
                    Debug.Log("Imagen estilizada aplicada al Canvas.");
                }
            }
            else
            {
                Debug.LogError($"Error al conectar con el servidor: {request.error}");
                Debug.LogError($"Detalles: {request.downloadHandler.text}");
            }
        }
    }
}

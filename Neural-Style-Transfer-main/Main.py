import sys
import matplotlib.pylab as plt
from API import transfer_style

if __name__ == "__main__":
    # Verificar si se pasa al menos un argumento para la imagen de contenido
    if len(sys.argv) < 2:
        print("Uso: python Main.py <content_image_path>")
        sys.exit(1)

    # Leer la imagen de contenido desde los argumentos
    content_image_path = sys.argv[1]

    # Rutas predeterminadas para estilo y salida
    style_image_path = r"C:\Users\juanc\Downloads\archive\Flowers299\Abutilon\0c32f4ce6e.jpg"
    output_image_path = r"C:\Users\juanc\Downloads\Neural-Style-Transfer-main\stylized_image.jpeg"

    # Ruta del modelo preentrenado
    model_path = r"C:\Users\juanc\Downloads\Neural-Style-Transfer-main\Neural-Style-Transfer-main\model"

    print(f"Procesando transferencia de estilo con los siguientes parámetros:")
    print(f" - Imagen de contenido: {content_image_path}")
    print(f" - Imagen de estilo: {style_image_path} (predeterminada)")
    print(f" - Imagen de salida: {output_image_path} (predeterminada)")

    # Aplicar transferencia de estilo
    img = transfer_style(content_image_path, style_image_path, model_path)

    # Guardar la imagen generada
    plt.imsave(output_image_path, img)
    print(f"Imagen estilizada guardada en: {output_image_path}")

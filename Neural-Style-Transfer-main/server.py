from flask import Flask, request, jsonify, send_file
import os
import matplotlib.pyplot as plt
from API import transfer_style
import io

app = Flask(__name__)

# Carpeta temporal para guardar imágenes
UPLOAD_FOLDER = 'uploaded_images'
os.makedirs(UPLOAD_FOLDER, exist_ok=True)
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER

MODEL_PATH = r"C:\Users\juanc\Documents\GitHub\JardinVirtual\Neural-Style-Transfer-main\model"

@app.route('/stylize-image', methods=['POST'])
def stylize_image():
    try:
        # Verificar si las imágenes fueron enviadas
        content_file = request.files.get('content_image')
        style_file = request.files.get('style_image')

        if not content_file or not style_file:
            return jsonify({'message': 'Faltan imágenes de contenido o estilo'}), 400

        # Guardar las imágenes temporalmente
        content_path = os.path.join(app.config['UPLOAD_FOLDER'], 'content_image.jpg')
        style_path = os.path.join(app.config['UPLOAD_FOLDER'], 'style_image.jpg')
        output_path = os.path.join(app.config['UPLOAD_FOLDER'], 'stylized_image.jpg')

        content_file.save(content_path)
        style_file.save(style_path)

        # Procesar la transferencia de estilo
        print("Procesando transferencia de estilo...")
        stylized_image = transfer_style(content_path, style_path, MODEL_PATH)

        # Guardar la imagen estilizada
        plt.imsave(output_path, stylized_image)

        # Enviar la imagen estilizada de vuelta
        with open(output_path, "rb") as f:
            return send_file(io.BytesIO(f.read()), mimetype='image/jpeg')

    except Exception as e:
        return jsonify({'message': str(e)}), 500

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000)

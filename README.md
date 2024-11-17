### Jardín Virtual: Neural Style Transfer en Unity

Jardín Virtual es una aplicación interactiva desarrollada con Unity y Flask que permite a los usuarios aplicar estilos artísticos a sus imágenes mediante transferencia de estilo neural. Este proyecto combina tecnologías de desarrollo de videojuegos e inteligencia artificial para proporcionar una experiencia visual inmersiva.

###Características
-Selección interactiva de imágenes: Los usuarios pueden seleccionar imágenes desde su dispositivo a través de una interfaz intuitiva.

-Amplia gama de estilos artísticos: Incluye hasta 10 opciones de estilos predefinidos.

-Transferencia de estilo neural: Transformación de imágenes utilizando un modelo preentrenado de aprendizaje profundo (GAN).

-Visualización en tiempo real: Renderizado directo del resultado estilizado en un entorno virtual (Flask).

# Capturas



# Instalación

####Clona el repositorio

`git clone https://github.com/Juancribas24/JardinVirtual.git`

####Instalar dependencias del backend
`cd Neural-Style-Transfer-main`

`pip install -r requirements.txt`

####Abrir el proyecto en Unity
-Inicia Unity Hub.

-Agrega el proyecto descargado a la lista de proyectos.

-Asegúrate de tener Unity 2022.3.17f1 LTS o superior.

####Configurar Flask
Navega a la carpeta Neural-Style-Transfer-main.

Ejecuta el servidor Flask

`python server.py`

El servidor estará disponible en http://127.0.0.1:5000.

####Configurar Unity
Arrastra las imágenes de estilo al arreglo styleImages del script ImageUploader en el Inspector.

Configura los botones de la UI para seleccionar estilos mediante el método SelectStyle.

##Uso

Seleccionar imagen: Usa el botón "cargar" para abrir el explorador de archivos y elegir una imagen.
Seleccionar estilo: Haz clic en uno de los botones de estilo disponibles.

Generar imagen estilizada: Presiona el botón "enviar" subir la imagen a Flask y procesar la imagen.

Visualizar resultado: La imagen estilizada se mostrará en el Canvas que tiene una RawImage de Unity.


Contacto
Creador: [Juancribas]
📧 Correo: [juancamilo927@gmail.com]
📧 Correo: [juancamilo927@gmail.com]
📧 Correo: [juancamilo927@gmail.com]

¡Gracias por explorar este proyecto! 🚀

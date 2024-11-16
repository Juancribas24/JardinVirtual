import matplotlib.pylab as plt
from API import transfer_style


if __name__=="__main__":

    # Path of the pre-trained TF model 
    model_path = r"C:\Users\juanc\Downloads\Neural-Style-Transfer-main\Neural-Style-Transfer-main\model"

    # NOTE : Works only for '.jpg' and '.png' extensions,other formats may give error
    content_image_path = r"C:\Users\juanc\Downloads\archive\Flowers299\AfricanDaisy\00ee32228b.jpg"
    style_image_path = r"C:\Users\juanc\Downloads\archive\Flowers299\Abutilon\0c32f4ce6e.jpg"

    img = transfer_style(content_image_path,style_image_path,model_path)
    # Saving the generated image
    plt.imsave('stylized_image.jpeg',img)
    plt.imshow(img)
    plt.show()


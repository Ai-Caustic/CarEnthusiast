const input = document.getElementById("add-img");
const output = document.getElementById("add-img-label");
let images = [];

input.addEventListener("change", function (event) {
    
    const imageFiles = event.target.files;

    totalImages = imageFiles.length;

    output.innerHTML = "";
    if (totalImages > 0) {
           for (const imageFile of imageFiles) {
        const reader = new FileReader();

        //Convert each image to a string
        reader.readAsDataURL(imageFile);

        // FileReader will emit the load event when the data URL is ready
        // Access the string using reader.result inside the callback function
        reader.addEventListener("load", () => {
            var img = document.createElement("img");
            img.setAttribute('class', "img-thumbnail");
            img.src = reader.result;
            input.parentNode.insertBefore(img, input);
        });
    }
    }
    else {
        output.innerHTML = "";
    }
});
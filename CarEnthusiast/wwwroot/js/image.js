const imgInputHelper = document.getElementById("add-single-img");
const imgInputHelperLabel = document.getElementById("add-img-label");
const imgContainer = document.querySelector(".custom__image-container");
const imgDisplayHelper = document.getElementById("display-single-img");
const imgDisplayHelperLabel = document.getElementById("display-img-label");
const displayContainer = document.querySelector(".display__image-container");
const imgFiles = [];

//imgInputHelper.style.display = "none";

const addImgHandler = () => {
  const file = imgInputHelper.files[0];

  if (!file) return;

  // Generate Image preview
  const reader = new FileReader();
  reader.readAsDataURL(file);

  reader.onload = () => {
    const newImg = document.createElement("img");

    newImg.src = reader.result;
    imgContainer.insertBefore(newImg, imgInputHelperLabel);
  };

  // Store img file
  imgFiles.push(file);

  //Reset image input
  imgInputHelper.value = "";

  return;
};

const getImgFileList = (imgFiles) => {
  const imgFilesHelper = new DataTransfer();
  imgFiles.forEach((imgFile) => imgFilesHelper.items.add(imgFile));

  return imgFilesHelper.files;
};

imgInputHelper.addEventListener("change", addImgHandler);

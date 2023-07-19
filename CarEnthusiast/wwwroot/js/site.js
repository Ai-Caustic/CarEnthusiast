//function validateLogin() {
//    let email = document.getElementById("login-email");
//    let password = document.getElementById("login-password");

//    let emailSpan = document.getElementById("login-email-span");
//    let passwordSpan = document.getElementById("login-password-span");

//    if (email == null) {
//        emailSpan.innerHTML = "The email is required";
//    }
//    else if (password == null) {
//        passwordSpan.innerHTML = "Password required";
//    }
//    else if (email && password == null) {
//        window.alert("Fill in the required fields");
//    }
//    else {
//        console.log("Login verified");
//    }

//}

//function validateRegister() {
//    // TODO

//    let name;
//    let email;
//    let password;
//    let confirmPassword;

//    let nameSpan;
//    let emailSpan;
//    let passwordSpan;
//    let confirmSpan;

//    if (name == null) {
//        nameSpan.innerHTML = "Name is required";
//    }
//    else if (email == null) {
//        emailSpan.innerHTML = "Email is required";

//    }
//    else if (password == null) {
//        passwordSpan.innerHTML = "Password is required";
//    }
//    else if (password != confirmPassword || confirmPassword == null) {
//        confirmSpan.innerHtml = "Passwords do not match";
//    }
//    else {
//        console.log(`User ${name} registered`);
//    }
//}

//let loginSubmit = document.getElementById("submit-login");

//let registerSubmit = document.getElementById("submit-register");

//loginSubmit.addEventListener("click", validateLogin);

//registerSubmit.addEventListener("click", validateRegister);

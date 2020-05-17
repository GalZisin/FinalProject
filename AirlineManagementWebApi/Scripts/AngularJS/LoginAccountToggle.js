$(document).ready(() => {
    console.log("Loaded");
    $('.message .a').click(() => {
        $('.ifExistAccount').animate({ height: "toggle", opacity: "toggle" }, { duration: "slow" });
    });

});
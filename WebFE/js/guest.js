document.addEventListener("DOMContentLoaded", function() {
    const buttons = document.querySelectorAll(".p-long-button");
    buttons.forEach(button => {
        button.addEventListener("click", () => {
            window.location.href = 'court-detail.html';
        });
    });
});

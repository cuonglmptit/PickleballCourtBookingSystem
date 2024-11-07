let slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    let i;
    let slides = document.getElementsByClassName("mySlides");
    let dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}

// Lấy phần tử nút và layout form
const bookingButton = document.querySelector('.p-long-button');
const bookingFormOverlay = document.querySelector('.hidden-overlay');

// Thêm sự kiện click cho nút
bookingButton.addEventListener('click', () => {
    bookingFormOverlay.style.display = 'block'; // Hiển thị form
});

// Tùy chọn: Thêm sự kiện đóng form khi bấm ngoài form hoặc bấm nút đóng
bookingFormOverlay.addEventListener('click', (event) => {
    if (event.target === bookingFormOverlay) {
        bookingFormOverlay.style.display = 'none'; // Ẩn form khi bấm ra ngoài
    }
});

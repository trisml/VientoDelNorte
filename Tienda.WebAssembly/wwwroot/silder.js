const slides = document.querySelectorAll('.slide');
const next = document.querySelector('#next');
const prev = document.querySelector('#prev');
const auto = true;
const intervalTime = 5000;
let slideInterval;

// Next Slide Function
const nextSlide = () => {
    // Get the current class
    const current = document.querySelector('.current');
    // Check if current is not null
    if (current) {
        // Remove the current class
        current.classList.remove('current');
        if (current.nextElementSibling) {
            // Add the current class to the next slide
            current.nextElementSibling.classList.add('current');
        } else {
            // Add the current class to the first when done from all
            slides[0].classList.add('current');
        }
        setTimeout(() => current.classList.remove('current'));
    }
}

// Previous Slide Function
const prevSlide = () => {
    // Get the current class
    const current = document.querySelector('.current');
    // Check if current is not null
    if (current) {
        // Remove the current class
        current.classList.remove('current');
        if (current.previousElementSibling) {
            // Add the current class to the previous slide
            current.previousElementSibling.classList.add('current');
        } else {
            // Add the current class to the last when done from all
            slides[slides.length - 1].classList.add('current');
        }
        setTimeout(() => current.classList.remove('current'));
    }
}

// Button Events
next.addEventListener('click', e => {
    nextSlide();
    if (auto) {
        // Clear interval time
        clearInterval(slideInterval);
        // Run next slide at interval time
        slideInterval = setInterval(nextSlide, intervalTime);
    }
});

prev.addEventListener('click', e => {
    prevSlide();
    if (auto) {
        // Clear interval time
        clearInterval(slideInterval);
        // Run next slide at interval time
        slideInterval = setInterval(nextSlide, intervalTime);
    }
});

// Auto Slide
if (auto) {
    // Run next slide at interval time
    slideInterval = setInterval(nextSlide, intervalTime);
}
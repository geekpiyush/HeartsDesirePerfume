let index = 0;
const carousel = document.querySelector(".carousel");
const items = document.querySelectorAll(".carousel-item");
const totalItems = items.length;

function updateCarousel() {
  const offset = -index * 100;
  carousel.style.transform = `translateX(${offset}%)`;
}

function nextSlide() {
  index = (index + 1) % totalItems;
  updateCarousel();
}

function prevSlide() {
  index = (index - 1 + totalItems) % totalItems;
  updateCarousel();
}

setInterval(nextSlide, 3000);


console.log("Working");
document.addEventListener('DOMContentLoaded', function() {
    const heartsContainer = document.getElementById('hearts-container');
    const yesButton = document.getElementById('yesButton');
    const noButton = document.getElementById('noButton');

    // Create falling hearts
    function createHeart() {
        const heart = document.createElement('div');
        heart.classList.add('heart');
        heart.innerHTML = '❤️';
        heart.style.left = Math.random() * 100 + 'vw';
        heart.style.animationDuration = Math.random() * 2 + 3 + 's';
        heartsContainer.appendChild(heart);

        setTimeout(() => {
            heart.remove();
        }, 5000);
    }

    setInterval(createHeart, 300);

    // Handle No button click
    noButton.addEventListener('click', function() {
        const yesButtonWidth = yesButton.offsetWidth;
        const yesButtonHeight = yesButton.offsetHeight;
        yesButton.style.width = yesButtonWidth + 20 + 'px';
        yesButton.style.height = yesButtonHeight + 10 + 'px';

        const noButtonWidth = noButton.offsetWidth;
        const noButtonHeight = noButton.offsetHeight;
        const maxX = window.innerWidth - noButtonWidth;
        const maxY = window.innerHeight - noButtonHeight;
        const randomX = Math.floor(Math.random() * maxX);
        const randomY = Math.floor(Math.random() * maxY);

        noButton.style.position = 'absolute';
        noButton.style.left = randomX + 'px';
        noButton.style.top = randomY + 'px';
    });

    // Handle Yes button click
    yesButton.addEventListener('click', function() {
        alert('Kyaaaaa lab na kita nyan❤️');
    });
});
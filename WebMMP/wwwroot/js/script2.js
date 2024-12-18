document.querySelectorAll('.room').forEach(room => {
    room.addEventListener('click', async function () {
        const roomTitle = this.getAttribute('data-room');
        const roomNumber = roomTitle.split(" ")[0];
        const equipment = this.getAttribute('data-equipment');
        const size = this.getAttribute('data-size');
        const book = await GetBook(roomNumber);
        var occupation = "";
        switch(book)
        {
            case 0:
                occupation = "Свободна";
                break;
            case 1:
                occupation = "Идет пара";
                break;
            case 2:
                occupation = "Занята";
                break;
        }
        document.getElementById('room-title').textContent = `Аудитория ${roomTitle}`;
        document.getElementById('room-info').innerHTML = `
            <strong>Статус:</strong> <span class="status" id="status">${occupation}</span><br>
            <strong>Оборудование:</strong> ${equipment}<br>
            <strong>Размер:</strong> ${size}
        `;
        const statusElement = document.getElementById("status");
        switch (statusElement.innerText) {
            case "Свободна":
                statusElement.classList.remove("occupied");
                statusElement.classList.remove("lesson");
                break;
            case "Идет пара":
                statusElement.classList.remove("occupied");
                statusElement.classList.add("lesson");
                break;
            case "Занята":
                statusElement.classList.remove("lesson");
                statusElement.classList.add("occupied");
                break;
        }

        switch (book) {
            case 0:
                this.classList.remove("lesson");
                this.classList.remove("occupied");
                this.classList.add("free");
                break;
            case 1:
                this.classList.remove("free");
                this.classList.remove("occupied");
                this.classList.add("lesson");
                break;
            case 2:
                this.classList.remove("free");
                this.classList.remove("lesson");
                this.classList.add("occupied");
                break;
        }

        document.getElementById('popup').style.display = 'flex';
    });
});

UpdateBook();

function closePopup() {
    document.getElementById('popup').style.display = 'none';
}

function toggleStatus() {
    const statusElement = document.getElementById("status");
    const roomTitle = document.getElementById("room-title").innerText;
    const roomNumber = roomTitle.split(" ")[1]; // Получаем номер аудитории из заголовка
    
    if (statusElement.innerText === "Свободна") {
        toggleBook(statusElement, roomNumber);
    } else {
        toggleUnbook(statusElement, roomNumber);
    }
}

async function toggleBook(statusElement, roomNumber) {
    console.log(roomNumber);
    const status = await SetBook(roomNumber, true);
    const room = document.getElementById(roomNumber);
    if (status == 0) {
        statusElement.innerText = "Занята";
        statusElement.classList.add("occupied");
        room.classList.remove("free");
        room.classList.add("occupied");
    }
}

async function toggleUnbook(statusElement, roomNumber) {
    const status = await SetBook(roomNumber, false);
    const room = document.getElementById(roomNumber);
    if (status == 0) {
        statusElement.innerText = "Свободна";
        statusElement.classList.remove("occupied");
        room.classList.remove("occupied");
        room.classList.add("free");
    }
}


async function UpdateBook() {
    data = await GetAllBooks();

    data.forEach((roomData) => {
        const room = document.getElementById(roomData.name);

        if (room) {
            switch (roomData.book) {
                case 0:
                    room.classList.add("free");
                    break;
                case 1:
                    room.classList.add("lesson");
                    break;
                case 2:
                    room.classList.add("occupied");
                    break;
            }
        }
    });
}

async function GetBook(number) {
    try {
        // Делаем GET-запрос к API
        const response = await fetch(`/api/values/${number}`); // Подставляем number в URL
        if (!response.ok) {
            throw new Error(`Ошибка: ${response.status}`);
        }
        const data = await response.json();
        return data.book;
    } catch (error) {
        console.error('Ошибка при запросе:', error);
        return null; // Возвращаем null в случае ошибки
    }
}

async function GetAllBooks() {
    try {
        // Делаем GET-запрос к API
        const response = await fetch(`/api/values/`); // Подставляем number в URL
        if (!response.ok) {
            throw new Error(`Ошибка: ${response.status}`);
        }
        const data = await response.json();
        return data;
    } catch (error) {
        console.error('Ошибка при запросе:', error);
        return null; // Возвращаем null в случае ошибки
    }
}

async function SetBook(number, value) {
    try {
        const response = await fetch(`/api/values/${number}/${value}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
        });
        if (!response.ok) {
            throw new Error(`Ошибка: ${response.status}`);
        }
        const data = await response.text();
        console.log('Прошло успешно');
        return data;
        
    } catch (error) {
        console.error('Ошибка при запросе:', error);
        return null
    }
}

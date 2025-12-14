using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        public HomeController() {}

        [HttpGet]
        [Route("/")] 
        public IActionResult Index()
        {
            return Content(@"
        <!DOCTYPE html>
        <html lang='ru'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Library API</title>
            <style>
                body { 
                    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; 
                    margin: 0; 
                    padding: 40px; 
                    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                    min-height: 100vh;
                    color: #333;
                }
                .container { 
                    max-width: 1000px; 
                    margin: 0 auto; 
                    background: white;
                    padding: 40px;
                    border-radius: 20px;
                    box-shadow: 0 20px 60px rgba(0,0,0,0.3);
                }
                h1 { 
                    color: #2c3e50; 
                    text-align: center;
                    margin-bottom: 40px;
                    font-size: 2.8em;
                    border-bottom: 3px solid #3498db;
                    padding-bottom: 20px;
                }
                .card { 
                    border-left: 5px solid #3498db; 
                    padding: 25px; 
                    margin: 20px 0; 
                    border-radius: 10px;
                    background: #f8f9fa;
                    transition: transform 0.3s, box-shadow 0.3s;
                }
                .card:hover {
                    transform: translateY(-5px);
                    box-shadow: 0 10px 30px rgba(0,0,0,0.15);
                }
                h2 { 
                    color: #2980b9; 
                    margin-top: 0;
                    font-size: 1.8em;
                }
                ul {
                    list-style-type: none;
                    padding-left: 0;
                }
                li {
                    margin: 12px 0;
                    padding: 12px;
                    background: white;
                    border-radius: 8px;
                    border: 1px solid #e0e0e0;
                    transition: all 0.3s;
                }
                li:hover {
                    background: #e3f2fd;
                    border-color: #3498db;
                }
                a { 
                    color: #0066cc; 
                    text-decoration: none; 
                    font-weight: 500;
                    align-items: center;
                    font-size: 1.1em;
                }
                a:hover { 
                    color: #ff6b6b;
                    text-decoration: underline;
                }
                a::before {
                    content: '🔗';
                    margin-right: 10px;
                    font-size: 0.9em;
                }
                .endpoint-type {
                    display: inline-block;
                    padding: 3px 8px;
                    border-radius: 4px;
                    font-size: 0.8em;
                    font-weight: bold;
                    margin-right: 10px;
                    color: white;
                }
                .get { background: #28a745; }
                .post { background: #007bff; }
                .put { background: #ffc107; color: #333; }
                .delete { background: #dc3545; }
                .status {
                    float: right;
                    padding: 5px 15px;
                    border-radius: 20px;
                    font-size: 0.9em;
                    font-weight: bold;
                }
                .online { background: #d4edda; color: #155724; }
                .header {
                    display: flex;
                    justify-content: space-between;
                    align-items: center;
                    margin-bottom: 30px;
                }
                .logo {
                    font-size: 2.5em;
                    font-weight: bold;
                    background: linear-gradient(45deg, #667eea, #764ba2);
                    -webkit-background-clip: text;
                    -webkit-text-fill-color: transparent;
                }
                .time {
                    color: #7f8c8d;
                    font-size: 0.9em;
                }
                .footer {
                    text-align: center;
                    margin-top: 40px;
                    padding-top: 20px;
                    border-top: 1px solid #eee;
                    color: #95a5a6;
                    font-size: 0.9em;
                }
                @media (max-width: 768px) {
                    body { padding: 20px; }
                    .container { padding: 20px; }
                    h1 { font-size: 2em; }
                }
            </style>
        </head>
        <body>
            <div class='container'>
                <div class='header'>
                    <div class='logo'>📚 LibraryAPI</div>
                    <div class='status online'>● ONLINE</div>
                </div>
                
                <h1>Система управления библиотекой</h1>
                
                <div class='card'>
                    <h2>📖 Документация API</h2>
                    <p>Полная интерактивная документация доступна через Swagger UI:</p>
                    <p><a href='/swagger' target='_blank'>🔍 Открыть Swagger UI</a></p>
                </div>
                
                <div class='card'>
                    <h2>📊 Основные ресурсы</h2>
                    <ul>
                        <li>
                            <span class='endpoint-type get'>GET</span>
                            <a href='/api/books' target='_blank'>/api/books</a> - Управление книгами
                        </li>
                        <li>
                            <span class='endpoint-type get'>GET</span>
                            <a href='/api/authors' target='_blank'>/api/authors</a> - Авторы книг
                        </li>
                        <li>
                            <span class='endpoint-type get'>GET</span>
                            <a href='/api/users' target='_blank'>/api/users</a> - Пользователи системы
                        </li>
                        <li>
                            <span class='endpoint-type get'>GET</span>
                            <a href='/api/genres' target='_blank'>/api/genres</a> - Жанры литературы
                        </li>
                        <li>
                            <span class='endpoint-type get'>GET</span>
                            <a href='/api/collections' target='_blank'>/api/collections</a> - Коллекции книг
                        </li>
                    </ul>
                </div>
                
                <div class='card'>
                    <h2>⚙️ Системные endpoints</h2>
                    <ul>
                        <li>
                            <span class='endpoint-type get'>GET</span>
                            <a href='/health' target='_blank'>/health</a> - Проверка состояния сервиса
                        </li>
                        <li>
                            <span class='endpoint-type get'>GET</span>
                            <a href='/info' target='_blank'>/info</a> - Информация о системе
                        </li>
                    </ul>
                </div>
                
                <div class='card'>
                    <h2>🔧 Методы API</h2>
                    <p>Каждый ресурс поддерживает стандартные CRUD операции:</p>
                    <p>
                        <span class='endpoint-type get'>GET</span> - Получить данные<br>
                        <span class='endpoint-type post'>POST</span> - Создать новый элемент<br>
                        <span class='endpoint-type put'>PUT</span> - Обновить существующий элемент<br>
                        <span class='endpoint-type delete'>DELETE</span> - Удалить элемент
                    </p>
                </div>
                
                <div class='footer'>
                    <p>Library Management System API v1.0 | " + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + @"</p>
                    <p>Для подробной информации используйте <a href='/swagger' target='_blank'>Swagger документацию</a></p>
                </div>
            </div>
            
            <script>
                // Обновление времени в футере
                function updateTime() {
                    const now = new Date();
                    const dateStr = now.toLocaleDateString('ru-RU') + ' ' + now.toLocaleTimeString('ru-RU');
                    const timeElement = document.querySelector('.footer p:first-child');
                    if(timeElement) {
                        timeElement.innerHTML = 'Library Management System API v1.0 | ' + dateStr;
                    }
                }
                
                // Обновляем время каждую секунду 
                setInterval(updateTime, 1000);
                
                // Проверка статуса API
                async function checkApiStatus() {
                    try {
                        const response = await fetch('/health');
                        const statusElement = document.querySelector('.status');
                        if(response.ok) {
                            statusElement.className = 'status online';
                            statusElement.textContent = '● ONLINE';
                        } else {
                            statusElement.className = 'status offline';
                            statusElement.textContent = '● OFFLINE';
                        }
                    } catch (error) {
                        const statusElement = document.querySelector('.status');
                        statusElement.className = 'status offline';
                        statusElement.textContent = '● OFFLINE';
                    }
                }
                
                // Проверяем статус каждые 30 секунд
                setInterval(checkApiStatus, 30000);
                
                // Первоначальная проверка
                checkApiStatus();
            </script>
        </body>
        </html>", "text/html; charset=utf-8");
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new 
            { 
                status = "Healthy", 
                timestamp = DateTime.UtcNow,
                service = "Library API"
            });
        }

        [HttpGet("info")]
        public IActionResult Info()
        {
            return Ok(new
            {
                name = "Library Management System API",
                version = "1.0",
                description = "API для управления библиотекой книг",
                endpoints = new[]
                {
                    "/swagger - Документация API",
                    "/api/books - Книги",
                    "/api/authors - Авторы",
                    "/api/users - Пользователи",
                    "/api/genres - Жанры",
                    "/api/collections - Коллекции"
                }
            });
        }
    }
}
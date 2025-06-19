0 Для ознайомлення
Link на гугл драйв (доступ відкритий) https://drive.google.com/file/d/10ryy5DnktJQVLoFGUlN6kP_ht5ZeOatD/view?usp=sharing
Білд лежить в D:\Gamedev\Multi_Input_UI\Assets\BuildLast
Unity version - 2022.3.25

1. Код
Загалом - компактні модульні класи з акцентом на Single Responsibility.
Структура побудована навколо екранів (screens) та вкладок (tabs):
Усі екрани наслідуються від MenuScreen.cs

Усі вкладки наслідуються від ScreenTab.cs, вкладки від ScreenTab.cs.
Налаштування винесені в ScriptableObjects.
Для анімацій використовується DoTween.
Текст - TextMeshPro.

2. Дизайн
Мінімалістичний sci-fi стиль.
Усі елементи UI - прямокутники одного кольору.
Додаткові елементи — лого та VFX в тому ж стилі.
Використано три кольори: оранжевий, чорний, білий.

LoadScreen - як у останній Zelda
Settings Screen - подібний до Witcher 3

3. Функціонал
Вікна з кнопками та вкладками
Overlay-елементи для XBox / PS / PC динамічно змінюються в рантаймі при підключенні / відключенні пристроїв

4. Анімації
Title Screen:
- Зникаюче лого (анiмацiя по alpha)
- пульсючий оверлей - press A to continue (анiмацiя по alpha)
Main Menu Screen
- При натисканні Play - з анімацією з'являються кнопки (New Game, Continue, Load). Анімація: scale, alpha
- Плавне з'явлення панелі з VFX-кулею. Анімація: alpha
LoadScreen 
- Карусель для сейвів — прокручується колесиком миші або навігацією
Settings Screen
- Пульсуюче підсвічування активної настройки

5. Сетап VFX-кулі на Main Menu Screen
Куля рендериться окремою камерою в іншій сцені
Сцена має Bloom Post-processing
Камера виводить зображення в RenderTexture
RenderTexture використовується як звичайний UI Image з анімацією fade in / fade out через DoTween
На кулі висить скрипт який описує її рух (axis rotation, changing axis)

6. Арт
Лого згенероване AI
Куля - з платного паку шейдерів із Asset Store
UI-елементи - з безкоштовного UI-паку з Asset Store


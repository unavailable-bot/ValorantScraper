namespace ValorantScraper
{
    public interface IImageDisplay
    {
        FlowLayoutPanel FlowLayoutPanelImages { get; }
        void AddControl(Control control);
    }

    public partial class ValorantScraper : Form
    {
        private string imageFolderPath = @"Data\skins_Base";
        private string agentsFolderPath = @"Data\agents_Base";
        private string iconFolderPath = @"Data\icons_Base";

        private Dictionary<string, string> skinNameReplacements = new Dictionary<string, string>
        {
            { "K/TAC Blade", "K TAC Blade" },
            { "K/TAC Bulldog", "K TAC Bulldog" },
            { "K/TAC Operator", "K TAC Operator" },
            { "K/TAC Sheriff", "K TAC Sheriff" },
            { "K/TAC Vandal", "K TAC Vandal" },
            { "Prime//2.0 Phantom", "Prime 2.0 Phantom" },
            { "Prime//2.0 Bucky", "Prime 2.0 Bucky" },
            { "Prime//2.0 Frenzy", "Prime 2.0 Frenzy" },
            { "Prime//2.0 Odin", "Prime 2.0 Odin" },
            { "Prime//2.0 Karambit", "Prime 2.0 Karambit" },
            { "VCT LOCK//IN Misericórdia", "VCT LOCK IN Misericórdia" },
            { "FIRE/arm Classic", "FIREarm Classic" },
            { "Coalition: Cobra Frenzy", "Coalition Cobra Frenzy" },
            { "Coalition: Cobra Judge", "Coalition Cobra Judge" },
            { "Coalition: Cobra Marshal", "Coalition Cobra Marshal" },
            { "Coalition: Cobra Odin", "Coalition Cobra Odin" }
        };

        private Dictionary<string, string> agentsNameReplacements = new Dictionary<string, string>
        {
            { "KAY/O", "KAY O" }
        };

        private readonly Dictionary<string, Color> folderColors = new Dictionary<string, Color>
        {
            { "exc_knife", Color.FromArgb(150, 80, 250, 100) },
            { "exclusive", Color.FromArgb(150, 80, 250, 94) },
            { "m_knifes", Color.FromArgb(150, 255, 124, 0) },
            { "l_knifes", Color.FromArgb(150, 227, 110, 0) },
            { "e_knifes", Color.FromArgb(150, 201, 98, 0) },
            { "r_knifes", Color.FromArgb(150, 173, 85, 2) },
            { "c_knifes", Color.FromArgb(150, 148, 73, 1) },
            { "mythic", Color.FromArgb(150, 255, 215, 82) },
            { "legendary", Color.FromArgb(150, 207, 119, 37) },
            { "epic", Color.FromArgb(150, 201, 81, 171) },
            { "rare", Color.FromArgb(150, 44, 156, 125) },
            { "common", Color.FromArgb(150, 135, 235, 255) }
        };

        // Упрощаем иерархию
        private readonly List<string> folderOrder = new List<string>
        {
            "exc_knife",
            "exclusive",
            "m_knifes",
            "l_knifes",
            "e_knifes",
            "r_knifes",
            "c_knifes",
            "mythic",
            "legendary",
            "epic",
            "rare",
            "common"
        };

        public ValorantScraper()
        {
            InitializeComponent();

            // Добавляем значения в ComboBox для Region
            combRegion.Items.AddRange(new string[] { "AP", "EU", "NA", "LATAM", "BR", "KR", "TUR" });

            // Добавляем значения в ComboBox для Rank
            combRank.Items.AddRange(new string[] { "Unrated", "Ranked Ready", "Full Unranked Ranked Ready",
                                               "IRON", "BRONZE", "SILVER", "GOLD", "PLATINUM",
                                               "DIAMOND", "ASCENDANT", "IMMORTAL", "RADIANT" });

            // Добавляем значения в ComboBox для Episode
            combEpisode.Items.AddRange(new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });

            // Добавляем значения в ComboBox для Act
            combAct.Items.AddRange(new string[] { "1", "2", "3" });

            // Добавляем значения в ComboBox для Act
            combTier.Items.AddRange(new string[] { "1", "2", "3" });
        }



        private string? GetIconForFolder(string folderName)
        {
            switch (folderName)
            {
                case "exc_knife":
                case "exclusive":
                case "m_knifes":
                case "l_knifes":
                case "e_knifes":
                case "r_knifes":
                case "c_knifes":
                case "legendary":
                    return Path.Combine(iconFolderPath, "legendary.png");
                case "mythic":
                    return Path.Combine(iconFolderPath, "mythic.png");
                case "epic":
                    return Path.Combine(iconFolderPath, "epic.png");
                case "rare":
                    return Path.Combine(iconFolderPath, "rare.png");
                case "common":
                    return Path.Combine(iconFolderPath, "common.png");
                default:
                    return null; // Или путь к стандартной иконке
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            // Создаем новую форму для отображения изображений
            Form2 imageDisplayForm = new Form2();
            AgentsResult imageDisplayAgentsForm = new AgentsResult();

            // Получаем список скинов из поля ввода
            string[] skins = txtSkins.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            string[] agents = txtCharacters.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Список для хранения не найденных скинов
            List<string> unmatchedSkins = new List<string>();

            // Список для хранения отсортированных названий скинов
            List<string> sortedSkinNames = new List<string>();

            var skinDetailsList = new List<(string skinName, string imagePath, Color bgColor, string folderName)>();

            // Проход по каждому скину из списка
            foreach (string skin in skins)
            {
                // Проверяем, есть ли скин в словаре замен
                string searchSkinName = skinNameReplacements.ContainsKey(skin) ? skinNameReplacements[skin] : skin;

                // Используем метод для поиска изображения с учетом всех подкаталогов
                var (imagePath, bgColor, folderName) = FindImageBySkinName(searchSkinName);

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {

                    // Добавляем найденный скин в список с деталями
                    skinDetailsList.Add((skin, imagePath, bgColor, folderName));
                }
                else
                {
                    // Если изображение не найдено, добавляем его в список не найденных
                    unmatchedSkins.Add(skin);
                }
            }

            skinDetailsList = skinDetailsList.OrderBy(skinDetail => folderOrder.IndexOf(folderColors.FirstOrDefault(f => f.Value == skinDetail.bgColor).Key)).ToList();

            foreach (var skinDetail in skinDetailsList)
            {
                AddImageToUISkins(imageDisplayForm, skinDetail.imagePath, skinDetail.skinName, skinDetail.bgColor, skinDetail.folderName);
                sortedSkinNames.Add(skinDetail.skinName); // Сохраняем порядок отображенных скинов
            }

            foreach (string agent in agents)
            {
                // Проверяем, есть ли скин в словаре замен
                string searchAgentName = agentsNameReplacements.ContainsKey(agent) ? agentsNameReplacements[agent] : agent;

                // Используем метод для поиска изображения с учетом всех подкаталогов
                string imagePath = FindImageByAgentName(searchAgentName)!; // Не забывайте использовать корректный путь

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    // Вместо создания PictureBox и Label напрямую, вызываем метод AddImageToUI
                    AddImageToUI(imageDisplayAgentsForm, imagePath, agent);
                }
            }

            // Отображаем список не найденных скинов
            txtUnmatchedSkins.Text = string.Join(Environment.NewLine, unmatchedSkins);

            // Показываем новую форму
            imageDisplayForm.ShowDialog();
            imageDisplayAgentsForm.ShowDialog();

            // Генерируем шаблон
            string template = GenerateTemplate(sortedSkinNames);

            // Копируем шаблон в буфер обмена
            Clipboard.SetText(template);

            // Дополнительно можно показать сообщение пользователю, что шаблон скопирован
            MessageBox.Show("Шаблон скопирован в буфер обмена!", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string? FindImageByAgentName(string agentName)
        {
            // Поиск изображения по имени в указанной директории
            var allFiles = Directory.GetFiles(agentsFolderPath, "*.*", SearchOption.TopDirectoryOnly)
                .Where(f => f.EndsWith(".png") || f.EndsWith(".jpg")); // Допустимые расширения

            // Сравнение скина с файлами в папке
            foreach (var file in allFiles)
            {
                if (Path.GetFileNameWithoutExtension(file).Equals(agentName, StringComparison.OrdinalIgnoreCase))
                {
                    return file; // Возвращаем путь к найденному изображению
                }
            }

            return null; // Если изображение не найдено
        }

        private (string imagePath, Color bgColor, string folderName) FindImageBySkinName(string skinName)
        {

            // Перебираем папки скинов: common, rare, epic, legendary, mythic
            foreach (var folder in Directory.GetDirectories(imageFolderPath))
            {
                // Перебираем все подпапки в каждой папке
                foreach (var subfolder in Directory.GetDirectories(folder))
                {
                    // Формируем полный путь к файлу изображения
                    string skinFilePath = Path.Combine(subfolder, skinName + ".png"); // или другой формат файла, если нужно
                    if (File.Exists(skinFilePath))
                    {
                        // Определяем цвет на основе имени родительской папки
                        string folderName = Path.GetFileName(folder);
                        Color bgColor = folderColors.ContainsKey(folderName) ? folderColors[folderName] : Color.White;

                        return (skinFilePath, bgColor, folderName);
                    }
                }
            }

            return (null, Color.White, null)!; // Если не найдено, возвращаем null и белый цвет
        }

        /*
        private string FindImageBySkinName(string skinName)
        {
            // Поиск изображения по имени в указанной директории и всех её подкаталогах
            var allFiles = Directory.GetFiles(imageFolderPath, "*.*", SearchOption.AllDirectories)
                .Where(f => f.EndsWith(".png") || f.EndsWith(".jpg")); // Допустимые расширения

            // Сравнение скина с файлами в папке
            foreach (var file in allFiles)
            {
                if (Path.GetFileNameWithoutExtension(file).Equals(skinName, StringComparison.OrdinalIgnoreCase))
                {
                    return file; // Возвращаем путь к найденному изображению
                }
            }

            return null; // Если изображение не найдено
        }
        */

        private void AddImageToUISkins(IImageDisplay form, string imagePath, string skinName, Color bgColor, string folderName)
        {
            // Создание панели для фона
            Panel backgroundPanel = new Panel
            {
                BackColor = bgColor, // Цвет фона будет зависеть от папки
                Width = 150, // Размеры панели совпадают с размерами изображения
                Height = 150,
                Padding = new Padding(5) // Добавьте немного отступа, если нужно
            };

            // Создание PictureBox для отображения изображения
            PictureBox picBox = new PictureBox
            {
                Image = Image.FromFile(imagePath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150, // Задайте нужный размер
                Height = 150
            };

            // Добавление изображения на панель фона
            picBox.Dock = DockStyle.Fill; // Заставим изображение занимать всю панель
            backgroundPanel.Controls.Add(picBox);

            // Получаем иконку на основе названия папки
            string iconPath = GetIconForFolder(folderName)!;
            PictureBox iconBox = new PictureBox
            {
                Image = !string.IsNullOrEmpty(iconPath) ? Image.FromFile(iconPath) : null,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Size = new Size(20, 20) // Размер иконки
            };

            // Создание подписи с названием изображения
            Label lblName = new Label
            {
                Text = skinName,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 150 // Убедитесь, что ширина совпадает с PictureBox
            };

            // Создание нового FlowLayoutPanel для одной строки
            FlowLayoutPanel panelRow = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(5)
            };

            // Добавление фона и подписи в новый FlowLayoutPanel
            panelRow.Controls.Add(iconBox); // Добавляем иконку
            panelRow.Controls.Add(backgroundPanel); // Добавляем панель фона
            panelRow.Controls.Add(lblName);

            // Добавление строки в основной FlowLayoutPanel
            form.FlowLayoutPanelImages.Controls.Add(panelRow);
        }
        private void AddImageToUI(IImageDisplay form, string imagePath, string skinName)
        {
            // Создание PictureBox для отображения изображения
            PictureBox picBox = new PictureBox
            {
                Image = Image.FromFile(imagePath),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150, // Задайте нужный размер
                Height = 150
            };

            // Создание панели для фона
            Panel backgroundPanel = new Panel
            {
                BackColor = Color.FromArgb(150, 125, 153, 250), // Белый цвет фона
                Width = 150, // Размеры панели совпадают с размерами изображения
                Height = 150,
                Padding = new Padding(5) // Добавьте немного отступа, если нужно
            };

            // Добавление изображения на панель фона
            picBox.Dock = DockStyle.Fill; // Заставим изображение занимать всю панель
            backgroundPanel.Controls.Add(picBox);

            // Создание подписи с названием изображения
            Label lblName = new Label
            {
                Text = skinName,
                TextAlign = ContentAlignment.MiddleCenter,
                Width = 150 // Убедитесь, что ширина совпадает с PictureBox
            };

            // Создание нового FlowLayoutPanel для одной строки
            FlowLayoutPanel panelRow = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(5)
            };

            // Добавление изображения и подписи в новый FlowLayoutPanel
            panelRow.Controls.Add(backgroundPanel);
            panelRow.Controls.Add(lblName);

            // Добавление строки в основной FlowLayoutPanel
            form.FlowLayoutPanelImages.Controls.Add(panelRow);
        }

        private void btnTransform_Click(object sender, EventArgs e)
        {
            // Получаем список скинов с дублями из поля ввода
            string[] skinsWithDuplicates = txtDuplicates.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Убираем дубликаты и объединяем в один список
            var uniqueSkins = skinsWithDuplicates.Distinct().ToList();

            // Сортируем по названиям сетов
            var sortedSkins = uniqueSkins.OrderBy(skin => GetSkinSetName(skin)).ThenBy(skin => skin).ToList();

            // Обновляем текстовое поле с отсортированными скинами
            txtSkins.Text = string.Join(Environment.NewLine, sortedSkins);

            // Подсчитываем количество уникальных агентов и записываем его в txtAgentsCount
            txtSkinsCount.Text = uniqueSkins.Count.ToString();
        }

        private void btnTransformAgents_Click(object sender, EventArgs e)
        {
            // Получаем список скинов с дублями из поля ввода
            string[] agentsWithDuplicates = agentsDuplicates.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            // Убираем дубликаты и объединяем в один список
            var uniqueAgents = agentsWithDuplicates.Distinct().ToList();

            // Объединяем список скинов без дубликатов в строку без пропусков между строками
            txtCharacters.Text = string.Join(Environment.NewLine, uniqueAgents);

            // Подсчитываем количество уникальных агентов и записываем его в txtAgentsCount
            txtAgentsCount.Text = uniqueAgents.Count.ToString();
        }

        private string GenerateTemplate(List<string> sortedSkinNames)
        {
            ScreenshotsTextBox screenshotsTextBox = new ScreenshotsTextBox();

            screenshotsTextBox.ShowDialog();

            // Собираем данные из полей
            string level = txtLevel.Text;
            string skinsCount = txtSkinsCount.Text;
            string region = combRegion.SelectedItem?.ToString() ?? string.Empty;
            string screenshots = screenshotsTextBox.ScreenshotsText;
            string rank = combRank.SelectedItem?.ToString() ?? string.Empty;
            string episode = combEpisode.SelectedItem?.ToString() ?? string.Empty;
            string act = combAct.SelectedItem?.ToString() ?? string.Empty;
            string buddies = txtBuddies.Text.Replace(Environment.NewLine, " / ");
            string buddiesSection = !string.IsNullOrEmpty(buddies) ? $"=\n{buddies}\n=" : "=";
            string agentsCount = txtAgentsCount.Text;
            string rPoints = txtRPoints.Text;
            string vPoints = txtVPoints.Text;
            string totalVPoints = txtTotalVPoints.Text;
            string startPrice = txtStartPrice.Text;
            string price = txtPrice.Text;
            string skinsList = txtSkins.Text;
            string tier = this.combTier.SelectedItem?.ToString() ?? string.Empty;

            List<string> knifeFolders = new List<string>
            {
                "exc_knife",
                "m_knifes",
                "l_knifes",
                "e_knifes",
                "r_knifes",
                "c_knifes"
            };

            // Списки для хранения ножей и остальных скинов
            List<(string skin, string folder)> knifeSkinsWithFolder = new List<(string skin, string folder)>();
            List<string> otherSkins = new List<string>();

            sortedSkinNames = ReplaceSkinNamesBeforeSort(sortedSkinNames);

            // Разделяем ножи и другие скины
            foreach (string skin in sortedSkinNames)
            {
                bool isKnife = false;

                // Проверяем каждый skin по папкам в порядке их иерархии
                foreach (string knifeFolder in knifeFolders)
                {
                    string fullKnifeFolderPath = Path.Combine(imageFolderPath, knifeFolder);
                    if (IsSkinInKnifeFolder(skin, fullKnifeFolderPath))
                    {
                        knifeSkinsWithFolder.Add((skin, knifeFolder));  // Сохраняем скин и папку для сортировки по иерархии
                        isKnife = true;
                        break;
                    }
                }

                // Если это не нож, добавляем в список других скинов
                if (!isKnife)
                {
                    otherSkins.Add(skin);
                }
            }

            // Сортируем ножи по иерархии папок
            var sortedKnifeSkins = knifeSkinsWithFolder
                .OrderBy(knife => folderOrder.IndexOf(knife.folder))  // Сортировка по порядку в folderOrder
                .Select(knife => knife.skin)  // Выбираем только название скинов для итогового списка
                .ToList();

            otherSkins = ReplaceSkinNamesAfterSort(otherSkins);
            sortedKnifeSkins = ReplaceSkinNamesAfterSort(sortedKnifeSkins);

            // Форматируем списки скинов и ножей
            string formattedSkinsList = string.Join(Environment.NewLine, otherSkins);
            string formattedKnifeSkinsList = string.Join(Environment.NewLine, sortedKnifeSkins);

            // Проверяем, есть ли ножи
            string skinsSection = sortedKnifeSkins.Count > 0
                ? $"-------------------------GunSkins$\n{formattedSkinsList}\n\n-------------------------KnifeSkins$\n{formattedKnifeSkinsList}"
                : $"-------------------------GunSkins$\n{formattedSkinsList}";

            string knifeSkin = sortedKnifeSkins.Count > 0 ? sortedKnifeSkins[0] : "No Knife";
            string skin1 = otherSkins.Count > 0 ? otherSkins[0] : "No Skin";
            string skin2 = otherSkins.Count > 1 ? otherSkins[1] : "No Skin";
            string skin3 = otherSkins.Count > 2 ? otherSkins[2] : "No Skin";
            string skin4 = otherSkins.Count > 3 ? otherSkins[3] : "No Skin";

            // Шаблон с заменой значений
            string template = $@"
                -----------------------------------------------------------------------------------------------------------------------
                # {{{region}}} |LvL {level}| >Skins: {skinsCount} [{knifeSkin}/{skin1}/{skin2}/{skin3}/{skin4}] #
                =========================
                Screenshots(in the link past '.' before 'com') -------------> {screenshots}

                -------------------------------Details*
                Rank:   {rank} {tier} Episode {episode} Act {act}
                {buddiesSection}
                Agents Unlocked {agentsCount}
                =
                RPoints: {rPoints}
                VPoints: {vPoints}
                Total VPoints Spent: {totalVPoints}
                =

                {skinsSection}

                -----------------------------------------------------------------------------------------------------------------------
                (Start {startPrice}$)
                G2G: {price}
                ";

            return template;
        }

        // Новый метод для замены названий скинов
        private List<string> ReplaceSkinNamesBeforeSort(List<string> skins)
        {
            return skins.Select(skin =>
                skinNameReplacements.TryGetValue(skin, out string? replacement)
                ? replacement
                : skin).ToList();
        }

        private List<string> ReplaceSkinNamesAfterSort(List<string> skins)
        {
            // Создаем новый список для сохранения измененных названий
            List<string> replacedSkins = new List<string>();

            // Проходим по каждому скину в списке
            foreach (var skin in skins)
            {
                // Ищем значение в словаре по ключу, где ключ — это скин без символов /
                string originalSkin = skinNameReplacements.FirstOrDefault(x => x.Value.Equals(skin)).Key;

                // Если нашли соответствие, добавляем оригинальное название с символом /
                if (!string.IsNullOrEmpty(originalSkin))
                {
                    replacedSkins.Add(originalSkin);
                }
                else
                {
                    // Если нет соответствия, добавляем скин без изменений
                    replacedSkins.Add(skin);
                }
            }

            return replacedSkins;
        }

        private bool IsSkinInKnifeFolder(string skin, string knifeFolderPath)
        {
            if (!Directory.Exists(knifeFolderPath))
            {
                return false; // Если папка не существует, сразу возвращаем false
            }

            // Получаем все подкаталоги в папке ножей
            string[] subfolders = Directory.GetDirectories(knifeFolderPath);

            foreach (string subfolder in subfolders)
            {
                // Ищем изображения внутри подкаталогов
                string[] images = Directory.GetFiles(subfolder, "*.png"); // Предполагаем, что изображения в формате .png
                foreach (string image in images)
                {
                    string imageName = Path.GetFileNameWithoutExtension(image);
                    if (imageName.Equals(skin, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string GetSkinSetName(string skinName)
        {
            // Извлекаем название сета (до последнего пробела)
            int lastSpaceIndex = skinName.LastIndexOf(' ');
            if (lastSpaceIndex > 0)
            {
                return skinName.Substring(0, lastSpaceIndex);
            }
            return skinName; // Если пробелов нет, возвращаем полное название
        }

        private void ResetAllFields(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                // Если элемент управления - это TextBox, то очищаем текст
                if (control is TextBox textBox)
                {
                    textBox.Clear(); // Очистка текста
                }

                // Если элемент управления - это ComboBox, то сбрасываем выбранное значение
                if (control is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1; // Сбрасываем выбор
                    comboBox.Text = string.Empty; // Очистить текстовое отображение
                }

                // Если элемент управления содержит дочерние элементы, рекурсивно вызываем метод
                if (control.HasChildren)
                {
                    ResetAllFields(control);
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ResetAllFields(this);
        }
    }
}

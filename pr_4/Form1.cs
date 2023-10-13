using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace pr_4
{
    public partial class Form1 : Form
    {

        // Используйте этот объект Random, чтобы выбрать случайные значки для квадратов

        Random random = new Random();

        // Каждая из этих букв представляет собой интересный значок в шрифте Webdings,
        // и каждый значок появляется в этом списке дважды
        List<string> icons = new List<string>()
        {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
        };
        // firstClicked указывает на первый элемент управления Label
        // что игрок нажимает, но оно будет нулевым
        // если игрок еще не нажал на метку
        Label firstClicked = null;

        // SecondClicked указывает на второй элемент управления Label
        // что игрок нажимает
        Label secondClicked = null;
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }
        private void AssignIconsToSquares()
        {
            // TableLayoutPanel имеет 16 меток,
            // и список значков содержит 16 значков,
            // поэтому значок извлекается из списка случайным образом
            // и добавляем к каждой метке
            //Первая строка преобразует переменную control в метку с именем iconLabel.
            //Вторая строка представляет собой оператор if, проверяющий и обеспечивающий успешное выполнение преобразования. Если преобразование выполняется, выполняются операторы в операторе if.
            //Первая строка в операторе if создает переменную с именем randomNumber, содержащую случайное число, соответствующее одному из элементов списка значков.Здесь используется метод Next()
            //объекта Random. Метод Next возвращает случайное число.Эта строка также использует свойство Count списка значков для определения диапазона, из которого выбирается случайное число.
            //Следующая строка назначает один из элементов Text списка значков свойству метки.
            //Следующая строка скрывает значки. Эта строка будет закомментирована, так что вы сможете проверить оставшуюся часть кода, прежде чем продолжить работу.
            //Последняя строка в if инструкции удаляет значок, добавленный в форму из списка.
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {
            // Таймер включается только после двух несовпадающих
            // значки были показаны игроку,
            // поэтому игнорируем любые щелчки, если таймер работает
            if (timer1.Enabled == true)
                return;
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Если выбранная метка черная, игрок нажал
                // значок, который уже открыт --
                // игнорировать щелчок
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // Если firstClicked имеет значение null, это первый значок
                // в паре, на которую нажал игрок,
                // поэтому установите firstClicked на метку, которую игрок
                // нажали, изменяем цвет на черный и возвращаемся
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
                // Если игрок зайдет так далеко, таймер не сработает
                // запуск и значение firstClicked не равно нулю,
                // так что это должен быть второй значок, на который нажал игрок
                // Устанавливаем черный цвет
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Проверяем, выиграл ли игрок
                CheckForWinner();

                // Если игрок нажал два одинаковых значка, сохраните их
                // черный цвет и сброс firstClicked и SecondClicked
                // чтобы игрок мог щелкнуть другой значок
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // Если игрок зайдет так далеко, игрок
                // щелкнули два разных значка, поэтому запустим
                // таймер (который будет ждать три четверти
                // секунду, а затем скрываем значки)
                timer1.Start();
                //Код в начале метода проверяет, запущен ли таймер, обращаясь к значению свойству Enabled.
                //Если игрок выбирает первый и второй элемент управления Label и таймер запускается, выбор третьего элемента управления Label ни к чему не приведет.
                //Код в конце метода задает ссылочную переменную secondClicked для отслеживания второго элемента управления Label. И затем присваивает значку ярлыка черный цвет,
                //чтобы сделать его видимым. Затем таймер запускается в однократном режиме, 
                //то есть ожидает 750 миллисекунд и после этого вызывает одно событие Tick. Обработчик события Tick таймера скрывает два значка и сбрасывает ссылочные переменные
                //firstClicked и secondClicked. Теперь форма готова к тому,
                //чтобы игрок выбрал другую пару значков.
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Остановить таймер
            timer1.Stop();


            // Скрываем обе иконки
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Сбрасываем firstClicked и SecondClicked
            // поэтому в следующий раз, когда метка будет
            // нажали, программа знает, что это первый клик
            firstClicked = null;
            secondClicked = null;
            //Останавливает таймер, вызывая метод Stop().
            //Использует две ссылочные переменные, firstClicked и secondClicked, чтобы снова сделать невидимыми значки двух меток, которые выбрал игрок.
            //Сбрасывает значения ссылочных переменных firstClicked и secondClicked на null в C# и Nothing в Visual Basic.
        }
        private void CheckForWinner()
        {
            // Проходим по всем меткам в TableLayoutPanel,
            // проверяем каждый из них, чтобы увидеть, соответствует ли его значок
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }


            // Если цикл не вернулся, значит, он не нашел
            // любые несовпадающие значки
            // Это означает, что пользователь выиграл. Показать сообщение и закрыть форму
            MessageBox.Show("Минус несколько минут твоей жизни", "Поздравляю!");
            Close();
        }
    }
}
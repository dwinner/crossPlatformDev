using System;
using System.Collections.Generic;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Java.IO;
using Java.Security;
using FragmentV4 = Android.Support.V4.App.Fragment;
using DialogFragmentV4 = Android.Support.V4.App.DialogFragment;

namespace FlagQuiz.App
{
   /// <summary>
   ///    Логика приложения Flag Quiz
   /// </summary>
   public class MainActivityFragment : FragmentV4
   {
      private const string ErrorTag = nameof(MainActivityFragment);
      private const int FlagsInQuiz = 10;

      // Имена файлов с флагами  TODO: Check effectiveness for collection operations
      private readonly List<string> _fileNameList = new List<string>();

      // Страны текущей викторины TODO: Check effectiveness for collection operations
      private readonly List<string> _quizCountriesList = new List<string>();

      private TextView _answerTextView; // Для правильного ответа
      private string _correctAnswer; // Правильная страна для текущего флага
      private int _correctAnswers; // Кол-во правильных ответов
      private ImageView _flagImageView; // Для вывода флага
      private LinearLayout[] _guessLinearLayouts; // Строки с кнопками
      private int _guessRows; // Кол-во строк с кнопками вариантов
      private Handler _handler; // Для задержки загрузки следующего флага
      private TextView _questionNumberTextView; // Номер текущего вопроса
      private LinearLayout _quizLinearLayout; // Макет с викториной
      private SecureRandom _random; // Генератор случайных чисел
      private ISet<string> _regionsSet; // Регионы текущей викторины
      private Animation _shakeAnimation; // Анимация неправильного ответа
      private int _totalGuesses; // Кол-во попыток      

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         base.OnCreateView(inflater, container, savedInstanceState);
         var view = inflater.Inflate(Resource.Layout.fragment_main, container, false);
         _random = new SecureRandom();
         _handler = new Handler();

         // Загрузка анимации для неправильных ответов
         _shakeAnimation = AnimationUtils.LoadAnimation(Activity, Resource.Animation.incorrect_shake);
         _shakeAnimation.RepeatCount = 3; // Анимация повторяется три раза

         // Получение ссылок на компоненты UI
         _quizLinearLayout = view.FindViewById<LinearLayout>(Resource.Id.quizLinearLayout);
         _questionNumberTextView = view.FindViewById<TextView>(Resource.Id.questionNumberTextView);
         _flagImageView = view.FindViewById<ImageView>(Resource.Id.flagImageView);
         _guessLinearLayouts = new[]
         {
            view.FindViewById<LinearLayout>(Resource.Id.row1LinearLayout),
            view.FindViewById<LinearLayout>(Resource.Id.row2LinearLayout),
            view.FindViewById<LinearLayout>(Resource.Id.row3LinearLayout),
            view.FindViewById<LinearLayout>(Resource.Id.row4LinearLayout)
         };
         _answerTextView = view.FindViewById<TextView>(Resource.Id.answerTextView);

         // Настройка слушателей для кнопок ответов
         foreach (var layout in _guessLinearLayouts)
            for (var column = 0; column < layout.ChildCount; column++)
               if (layout.GetChildAt(column) is Button button)
                  button.Click += (sender, args) =>
                  {
                     var guess = button.Text.ToString();
                     var answer = GetCountryName(_correctAnswer);
                     ++_totalGuesses; // Увеличение кол-ва попыток пользователя

                     if (guess.Equals(answer, StringComparison.Ordinal)) // Если ответ правильный
                     {
                        _correctAnswers++; // Увеличить кол-во правильных ответов

                        // Правильный ответ выводится зелёным цветом
                        _answerTextView.Text = $"{answer}!";
                        _answerTextView.SetTextColor(Resources.GetColor(Resource.Color.correct_answer, Context.Theme));

                        DisableButtons(); // Блокировка всех кнопок ответов

                        // Если пользователь правильно угадал FLAGS_IN_QUIZ флагов
                        if (_correctAnswers == FlagsInQuiz)
                        {
                           // Вывод статистики и перезапуск
                           DialogFragmentV4 quizResults = new DialogFragmentImpl(this);

                           // Использование FragmentManager для вывода DialogFragment
                           quizResults.Cancelable = false;
                           quizResults.Show(FragmentManager, "quiz results");
                        }
                        else
                        {
                           // Ответ правильный, но викторина не закончена
                           // Загрузка следующего флага после 2-х секундной задержки
                           _handler.PostDelayed(() => Animate(true), 2000);
                        }
                     }
                     else
                     {
                        // Неправильный ответ
                        _flagImageView.StartAnimation(_shakeAnimation); // Встряхивание

                        // Сообщение "Incorrect!" выводится красным шрифтом
                        _answerTextView.SetText(Resource.String.incorrect_answer);
                        _answerTextView.SetTextColor(Resources.GetColor(Resource.Color.incorrect_answer,
                           Context.Theme));
                        button.Enabled = false; // Блокировка неправильного ответа
                     }
                  };

         // Назначение текста questionNumberTextView
         _questionNumberTextView.Text = GetString(Resource.String.question, 1, FlagsInQuiz);

         return view; // Представление фрагмента для вывода
      }

      private void DisableButtons()
      {
         for (var row = 0; row < _guessRows; row++)
         {
            var guessRow = _guessLinearLayouts[row];
            for (var i = 0; i < guessRow.ChildCount; i++) guessRow.GetChildAt(i).Enabled = false;
         }
      }

      /// <summary>
      ///    Обновление guessRows на основании значения SharedPreferences
      /// </summary>
      /// <param name="sharedPreferences">Объект постоянного хранилища</param>
      public void UpdateGuessRows(ISharedPreferences sharedPreferences)
      {
         // Получение количества отображаемых вариантов ответа
         var choices = sharedPreferences.GetString(MainActivity.Choices, null);
         _guessRows = int.Parse(choices) / 2;

         // Все компоненты LinearLayout скрываются
         foreach (var layout in _guessLinearLayouts) layout.Visibility = ViewStates.Gone;

         // Отображение нужных компонентов LinearLayout
         for (var row = 0; row < _guessRows; row++) _guessLinearLayouts[row].Visibility = ViewStates.Visible;
      }

      public void UpdateRegions(ISharedPreferences sharedPreferences)
         => _regionsSet = new HashSet<string>(sharedPreferences.GetStringSet(MainActivity.Regions, null));

      public void ResetQuiz()
      {
         // Использование AssetManager для получения имен файлов изображений
         var assets = Activity.Assets;
         _fileNameList.Clear();

         // Перебрать все регионы
         try
         {
            foreach (var region in _regionsSet)
            {
               var paths = assets.List(region);
               Array.ForEach(paths, path => _fileNameList.Add(path.Replace(".png", string.Empty)));
            }
         }
         catch (IOException ioEx)
         {
            Log.Error(ErrorTag, "Error loading image file names", ioEx);
         }
         finally
         {
            _correctAnswers = 0; // Сброс кол-ва правильных ответов
            _totalGuesses = 0; // Сброс общего кол-ва попыток
            _quizCountriesList.Clear(); // Очистка предыдущего списка стран
         }

         var flagCounter = 0;
         var numberOfFlags = _fileNameList.Count;

         // Добавление FLAGS_IN_QUIZ случайных файлов в quizCountriesList
         while (flagCounter <= FlagsInQuiz)
         {
            var randomIndex = _random.NextInt(numberOfFlags);

            // Получение случайного имени файла
            var fileName = _fileNameList[randomIndex];

            // Если регион включен, но ещё не был выбран
            if (!_quizCountriesList.Contains(fileName))
            {
               _quizCountriesList.Add(fileName); // Добавить файл в список
               ++flagCounter;
            }
         }

         LoadNextFlag(); // Запустить викторину загрузкой первого флага
      }

      private void LoadNextFlag() // Загрузка следующего флага после правильного ответа
      {
         // Получение имени файла следующего флага и удаление его из списка
         var nextImage = _quizCountriesList[0];
         _quizCountriesList.RemoveAt(0);
         _correctAnswer = nextImage; // Обновление правильного ответа
         _answerTextView.Text = string.Empty; // Очистка _answerTextView

         // Отображение номера текущего вопроса         
         _questionNumberTextView.Text = GetString(Resource.String.question, _correctAnswers + 1, FlagsInQuiz);

         // Извлечение региона из имени следующего изображения
         var region = nextImage.Substring(0, nextImage.IndexOf('-'));

         // Использование AssetManager для загрузки следующего изображения
         var assets = Activity.Assets;

         // Получение объекта InputStream для ресурса следующего флага и попытка использования InputStream
         try
         {
            using (var stream = assets.Open($"{region}/{nextImage}.png"))
            {
               // Загрузка графики в виде объекта Drawable и вывод на flagImageView
               var flag = Drawable.CreateFromStream(stream, nextImage);
               _flagImageView.SetImageDrawable(flag);
               Animate(false); // Анимация появления флага на экране
            }
         }
         catch (IOException ioEx)
         {
            Log.Error(ErrorTag, $"Error loading {nextImage}", ioEx);
         }

         _fileNameList.Shuffle(); // Перестановка имён файлов

         // Помещение правильного ответа в конец fileNameList
         var correct = _fileNameList.IndexOf(_correctAnswer);
         _fileNameList.RemoveAt(correct);
         _fileNameList.Add(_correctAnswer);

         // Добавление 2, 4, 6 или 8 кнопок в зависимости от значения guessRows
         for (var row = 0; row < _guessRows; row++)
            // Размещение кнопок в currentTableRow
         for (var column = 0; column < _guessLinearLayouts[row].ChildCount; column++)
            // Получение ссылки на Button
            if (_guessLinearLayouts[row].GetChildAt(column) is Button newGuessButton)
            {
               newGuessButton.Enabled = true;

               // Назначение названия страны текстом newGuessButton
               var fileName = _fileNameList[row * 2 + column];
               newGuessButton.Text = GetCountryName(fileName);
            }

         var nextRow = _random.NextInt(_guessRows); // Случайная замена одной кнопки правильным ответом
         var nextCol = _random.NextInt(2); // Выбор случайного столбца
         var randomRow = _guessLinearLayouts[nextRow]; // Получение строки
         var countryName = GetCountryName(_correctAnswer);
         if (randomRow.GetChildAt(nextCol) is Button choiceButton) choiceButton.Text = countryName;
      }

      private static string GetCountryName(string fileName)
         => fileName.Substring(fileName.IndexOf('-') + 1).Replace('_', ' ');

      private void Animate(bool animateOut) // Весь макет quizLinearLayout появляется или исчезает с экрана
      {
         // Предотвращение анимации интерфейса для первого флага
         if (_correctAnswers == 0) return;

         // Вычисление координат центра
         var centerX = (_quizLinearLayout.Left + _quizLinearLayout.Right) / 2;
         var centerY = (_quizLinearLayout.Top + _quizLinearLayout.Bottom) / 2;

         // Вычисление радиуса анимации
         var radius = Math.Max(_quizLinearLayout.Width, _quizLinearLayout.Height);

         Animator animator;

         // Если изображение должно исчезать с экрана
         if (animateOut)
         {
            // Создание круговой анимации
            animator = ViewAnimationUtils.CreateCircularReveal(_quizLinearLayout, centerX, centerY, radius, 0);
            animator.AnimationEnd += (sender, args) => LoadNextFlag();
         }
         else
         {
            // Если макет quizLinearLayout должен появиться
            animator = ViewAnimationUtils.CreateCircularReveal(_quizLinearLayout, centerX, centerY, 0, radius);
         }

         animator.SetDuration(500); // Анимация продолжительностью 500 мс
         animator.Start(); // Начало анимации
      }

      private sealed class DialogFragmentImpl : DialogFragmentV4
      {
         private readonly MainActivityFragment _fragment;

         public DialogFragmentImpl(MainActivityFragment fragment) => _fragment = fragment;

         public override Dialog OnCreateDialog(Bundle savedInstanceState)
            => new AlertDialog.Builder(Activity)
               .SetMessage(GetString(Resource.String.results, _fragment._totalGuesses,
                  1000 / (double) _fragment._totalGuesses))
               .SetPositiveButton(Resource.String.reset_quiz,
                  (sender, args) => _fragment.ResetQuiz()) // Кнопка сброса "Reset Quiz"
               .Create();
      }
   }
}
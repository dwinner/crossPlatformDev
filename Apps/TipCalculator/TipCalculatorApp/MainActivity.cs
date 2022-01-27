using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Text;

namespace AppDevUnited.TipCalculatorApp
{
   /// <summary>
   ///    Main activity
   /// </summary>
   [Activity(
      Label = "TipCalculatorApp",
      //Name = "AppDevUnited.TipCalculatorApp.MainActivity",
      MainLauncher = true,
      WindowSoftInputMode = SoftInput.StateAlwaysVisible,
      ScreenOrientation = ScreenOrientation.Portrait)]
   public class MainActivity : AppCompatActivity
   {
      private const double DefaultBillAmout = 0.0;
      private const double DefaultPercent = 0.15;

      // Форматировщики денежных сумм и процентов
      private static readonly NumberFormat _CurrencyFormat = NumberFormat.CurrencyInstance;
      private static readonly NumberFormat _PercentFormat = NumberFormat.PercentInstance;
      private TextView _amountTextView; // Для отформатированной суммы счета
      private double _billAmount = DefaultBillAmout; // Сумма счета, введенная пользователем
      private double _percent = DefaultPercent; // Исходный процент чаевых
      private TextView _percentTextView; // Для вывода процента чаевых
      private TextView _tipTextView; // Для вывода вычисленных чаевых
      private TextView _totalTextView; // Для вычисленной общей суммы

      /// <inheritdoc />
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Resource.Layout.Main);

         // Получение ссылок на компоненты TextView, с которыми активность взаимодействует на программном уровне
         _amountTextView = FindViewById<TextView>(Resource.Id.amountTextView);
         _percentTextView = FindViewById<TextView>(Resource.Id.percentTextView);
         _tipTextView = FindViewById<TextView>(Resource.Id.tipTextView);
         _totalTextView = FindViewById<TextView>(Resource.Id.totalTextView);

         _tipTextView.Text = _CurrencyFormat.Format(0);
         _totalTextView.Text = _CurrencyFormat.Format(0);

         // Назначение слушателя TextWatcher для amountEditText
         var amountEditText = FindViewById<EditText>(Resource.Id.amountEditText);
         amountEditText.TextChanged += OnAmountTextChanged;

         // Назначение слушателя OnSeekBarChangeListener для percentSeekBar
         var percentSeekBar = FindViewById<SeekBar>(Resource.Id.percentSeekBar);
         percentSeekBar.ProgressChanged += OnPercentProgressChanged;
      }

      private void OnPercentProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
      {
         _percent = e.Progress / 100.0;
         Calculate();
      }

      private void OnAmountTextChanged(object sender, TextChangedEventArgs e)
      {
         var changedText = e.Text.Aggregate(string.Empty, (current, @char) => current + @char);

         // Вызывается при изменении пользователем величины счета
         if (double.TryParse(changedText, out var billAmount))
         {
            _amountTextView.Text = _CurrencyFormat.Format(billAmount);
            _billAmount = billAmount;
         }
         else
         {
            _amountTextView.Text = string.Empty;
            _billAmount = default(double);
         }

         Calculate();
      }

      /// <summary>
      ///    Вычисление, вывод чаевых и общей суммы
      /// </summary>
      private void Calculate()
      {
         // Форматирование процентов и вывод в _percentTextView
         _percentTextView.Text = _PercentFormat.Format(_percent);

         // Вычисление чаевых и общей суммы
         var tip = _billAmount * _percent;
         var total = _billAmount + tip;

         // Вывод чаевых и общей суммы в формате денежной величины
         _tipTextView.Text = _CurrencyFormat.Format(tip);
         _totalTextView.Text = _CurrencyFormat.Format(total);
      }
   }
}
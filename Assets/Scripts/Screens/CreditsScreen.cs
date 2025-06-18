namespace Screens
{
    public class CreditsScreen: MenuScreen
    {
        public override void OnCancelClick()
        {
            base.OnCancelClick();
            UIController.Instance.ShowScreen(ScreenType.MainMenu);
        }
    }
}
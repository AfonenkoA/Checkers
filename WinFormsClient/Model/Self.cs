using WinFormsClient.Model.Item;

namespace WinFormsClient.Model;

public sealed class Self : User
{
    public IEnumerable<VisualAnimation> AvailableAnimations { get; }
    public IEnumerable<VisualCheckersSkin> AvailableCheckersSkins { get; }
    public IEnumerable<VisualLootBox> AvailableLootBoxes { get; }
    public int Currency;

    public IEnumerable<VisualAnimation> Animations { get; }
    public IEnumerable<VisualCheckersSkin> CheckersSkins { get; }
    public Self(User user,
        IEnumerable<VisualAnimation> availableAnimations,
        IEnumerable<VisualCheckersSkin> availableCheckersSkins,
        IEnumerable<VisualLootBox> availableLootBoxes,
        IEnumerable<VisualAnimation> animations,
        IEnumerable<VisualCheckersSkin> checkersSkins, int currency) : base(user)
    {
        AvailableAnimations = availableAnimations;
        AvailableCheckersSkins = availableCheckersSkins;
        AvailableLootBoxes = availableLootBoxes;
        Animations = animations;
        CheckersSkins = checkersSkins;
        Currency = currency;
    }
}
namespace RPG.Controller
{
    public interface IRaycastable
    {
        bool HandleRayCast(PlayerController callingController);
    }
}
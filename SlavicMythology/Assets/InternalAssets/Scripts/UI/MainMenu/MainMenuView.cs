using UnityEngine;
using VContainer;

public class MainMenuView : MonoBehaviour
{
    private MainMenuViewModel _viewModel;

    [Inject]
    public void Construct(MainMenuViewModel viewModel)
    {
        _viewModel = viewModel;
    }

    public void OnStartButtonClicked()
    {
        _viewModel.StartGame();
    }
}
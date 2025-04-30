using UnityEngine;
using Zenject;

namespace WeatherDogs
{
    public class Bootstrap : MonoBehaviour
    {
        private WindowMediator _mediator;

        [Inject]
        private void Construct(WindowMediator mediator) => _mediator = mediator;

        private void Start()
        {
            _mediator.Start();
        }
    }
}
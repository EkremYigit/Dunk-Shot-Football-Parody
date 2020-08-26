using UnityEngine;

namespace DefaultNamespace
{
    public class GeneralManager : MonoBehaviour
    {
        [SerializeField]
        private LevelManager levelManager;
        [SerializeField]
        private MapManager mapManager;
        [SerializeField]
        private BarrierManager barrierManager;
        [SerializeField]
        private Designs designManager;
        [SerializeField]
        private PlayerManager playerManager;
        [SerializeField]
        private BackgroundManager backgroundManager;
        [SerializeField]
        private ObjectAnimation objectAnimationManager;
        [SerializeField]
        private ObjectAnimation objectAnimationManager2;
        [SerializeField]
        private ObjectAnimation objectAnimationManager3;
        [SerializeField]
        private ObjectAnimation objectAnimationManager4;
        [SerializeField]
        private ObjectAnimation projectiles;
        [SerializeField]
        private ObjectAnimation airPlane;
        [SerializeField]
        private ObjectAnimation goalPostBlocker;
        public ObjectAnimation AirPlane
        {
            get => airPlane;
            set => airPlane = value;
        }

        public ObjectAnimation ObjectAnimationManager4
        {
            get => objectAnimationManager4;
            set => objectAnimationManager4 = value;
        }

        public MapManager MapManager
        {
            get => mapManager;
            set => mapManager = value;
        }


        public ObjectAnimation ObjectAnimationManager3
        {
            get => objectAnimationManager3;
            set => objectAnimationManager3 = value;
        }
        public ObjectAnimation ObjectAnimationManager2
        {
            get => objectAnimationManager2;
            set => objectAnimationManager2 = value;
        }

        public BackgroundManager BackgroundManager
        {
            get => backgroundManager;
            set => backgroundManager = value;
        }

        public ObjectAnimation ObjectAnimationManager
        {
            get => objectAnimationManager;
            set => objectAnimationManager = value;
        }
        public PlayerManager PlayerManager
        {
            get => playerManager;
            set => playerManager = value;
        }

        public LevelManager LevelManager
        {
            get => levelManager;
            set => levelManager = value;
        }

        public BarrierManager BarrierManager
        {
            get => barrierManager;
            set => barrierManager = value;
        }


        public Designs DesignManager
        {
            get => designManager;
            set => designManager = value;
        }

        public ObjectAnimation GoalPostBlocker
        {
            get => goalPostBlocker;
            set => goalPostBlocker = value;
        }

        public ObjectAnimation Projectiles
        {
            get => projectiles;
            set => projectiles= value;
        }
    }
}
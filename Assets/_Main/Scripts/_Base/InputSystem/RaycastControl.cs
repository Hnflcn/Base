
using _Main.Scripts.Managers;
using UnityEngine;

namespace _Main.Scripts._Base.InputSystem
{
    public class RaycastControl : MonoBehaviour
    {
        [SerializeField] protected LayerMask layerMaskSelectable;

        private GameplayReferences _references;
        
        public void Init()
        {
            _references = GameplayReferences.Instance;
        }
        
        
      // public (bool, Block) GetRaycastBlockPiece()
      // {
      //     var clicked = RayCasting.ReturnRayObject<BlockCollider>(layerMaskBlock, 200);
      //     
      //     if (clicked != null && _references.gameState == GameState.OnGame && clicked.CanClick)
      //         return (true, clicked.blockPiece.block);
      //     else
      //         return (false,null);
      // }
      // 
      // public (bool, Zone) GetRaycastZone()
      // {
      //     var clicked = RayCasting.ReturnRayObject<ZoneCollider>(layerMaskZone, 200);
      //     
      //     if (clicked != null && _references.gameState == GameState.OnGame && clicked.CanClick)
      //         return (true, clicked.zone);
      //     else
      //         return (false,null);
      // }
        
        public Vector2Int GetGridPositionFromScreen(Vector2 screenPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 worldPosition = hit.point;
                Vector2Int gridPosition = new Vector2Int(
                    Mathf.FloorToInt(worldPosition.x),
                    Mathf.FloorToInt(worldPosition.z)
                );
                return gridPosition;
            }
            return Vector2Int.zero; 
        }

    }
}
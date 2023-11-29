using System.Collections;
using UnityEngine;

public class EntityManager : MonoBehaviour {
    private Material _whiteMaterial;
    private Material _originalMaterial;
    private SpriteRenderer _spriteRenderer;

    protected EntityAnimations _animationController;
    protected bool _isDead = false;


    protected virtual void Awake()
    {
        this._animationController = gameObject.GetComponent<EntityAnimations>();
        this.InitBlinkMaterial();
    }

    private void InitBlinkMaterial() {
        this._whiteMaterial = new Material(Shader.Find("Unlit/Color"));
        this._spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        if (this._spriteRenderer) {
            this._originalMaterial = this._spriteRenderer.material;
        }
    }

    public virtual void EntityHit()
    {
        if (!this._isDead) {
            this._animationController.Hit();
            if (this._spriteRenderer) {
                StartCoroutine(BlinkEntity());
            }
        }
    }

    protected virtual void EntityDead() {
        this._animationController.Death();
        this._isDead = true;
    }

    private IEnumerator BlinkEntity() {
        this._spriteRenderer.material = this._whiteMaterial;
        yield return new WaitForSeconds(.2f);
        this._spriteRenderer.material = this._originalMaterial;
    }
}
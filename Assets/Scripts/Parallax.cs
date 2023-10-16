using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float moveSpeed;
    private PlatformMovement p;
    //private float currentX;

    [SerializeField] private float moveSpeedMultiplier;

    [SerializeField] private bool scrollRight;

    private float singleTextureWidth;
    // Start is called before the first frame update
    void Start()
    {
        SetupTexture();
        p = FindObjectOfType<PlatformMovement>();
        //currentX = transform.position.x;
    }

    private void SetupTexture()
    {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width * transform.localScale.x / sprite.pixelsPerUnit;
    }

    private void Scroll()
    {
        if (!scrollRight) moveSpeed = p.GetPlatformSpeed() * moveSpeedMultiplier;
        else moveSpeed = p.GetPlatformSpeed() * -moveSpeedMultiplier;
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
    }

    private void CheckReset()
    {
        if (Mathf.Abs(transform.position.x) - singleTextureWidth > 0)
        {
            //Assumes that our local X position is set to 0. Gets funky with currentX.
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        Scroll();
        CheckReset();
    }
}

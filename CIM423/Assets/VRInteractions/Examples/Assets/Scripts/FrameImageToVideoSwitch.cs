using UnityEngine;
using UnityEngine.Video;

public class FrameImageToVideoSwitch : MonoBehaviour
{
    [Header("Frame Display")]
    public Renderer frameRenderer;     // the screen mesh renderer
    public Sprite pictureSprite;       // <-- now accepts your 2D sprite
    public RenderTexture videoTexture; // FrameRT_x

    [Header("Video")]
    public VideoPlayer videoPlayer;
    public bool restartOnPress = true;

    [Header("Optional Sound")]
    public AudioSource sfxSource;
    public AudioClip pressClip;
    [Range(0f, 2f)] public float pressVolume = 1f;

    int texId;
    Texture pictureTexture;

    void Awake()
    {
        // Convert sprite → texture
        if (pictureSprite != null)
            pictureTexture = pictureSprite.texture;

        // URP vs Built-in shader support
        texId = Shader.PropertyToID("_BaseMap");
        if (frameRenderer != null && frameRenderer.material != null &&
            !frameRenderer.material.HasProperty(texId))
        {
            texId = Shader.PropertyToID("_MainTex");
        }

        ShowPicture();

        if (videoPlayer != null)
            videoPlayer.loopPointReached += OnVideoFinished;
    }

    public void Press()
    {
        if (sfxSource != null && pressClip != null)
            sfxSource.PlayOneShot(pressClip, pressVolume);

        ShowVideo();

        if (restartOnPress) videoPlayer.time = 0;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        ShowPicture();
    }

    void ShowPicture()
    {
        if (frameRenderer != null && pictureTexture != null)
            frameRenderer.material.SetTexture(texId, pictureTexture);
    }

    void ShowVideo()
    {
        if (frameRenderer != null && videoTexture != null)
            frameRenderer.material.SetTexture(texId, videoTexture);
    }
}

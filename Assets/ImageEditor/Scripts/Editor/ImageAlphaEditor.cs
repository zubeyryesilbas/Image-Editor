using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Object =  UnityEngine.Object;
public class ImageAlphaEditor : EditorWindow
{
    private ObjectField _textureField;
    private DropdownField _alphaDropDown ,  _textureOption;
    private GradientField _alphaGradient;
    private VisualElement _imagePreview , customTexValues;
    private SliderInt _alphaSlider;
    private Button _exportButton , _createTexButton;
    private string _outputName;
    private Texture2D _selectedTexture , _outputTexture;
    private IntegerField _widthField , _heightField , _alphaInput;
    private ColorField _tint;
    private ComputeShader _shader;

    [MenuItem("Tools/Image Alpha Editor")]
    public static void OpenEditorWindow()
    {
        ImageAlphaEditor window = GetWindow<ImageAlphaEditor>();
        window.titleContent = new GUIContent("Image Alpha Editor");
        window.maxSize = new Vector2(320 , 550);
        window.minSize = window.maxSize;
    
    }
    
    private void CreateGUI()
    {
        VisualElement root = rootVisualElement;
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ImageEditor/Resources/UI Documents/ImageAlphaEditor.uxml");
        var tree =  visualTree.Instantiate();
        root.Add(tree);

        _textureField = root.Q<ObjectField>("texture-field");
        _alphaDropDown = root.Q<DropdownField>("alpha-dropdown");
        _textureOption = root.Q<DropdownField>("texture-option");
        _alphaGradient = root.Q<GradientField>();
        _imagePreview = root.Q<VisualElement>("image-preview");
        _alphaSlider = root.Q<SliderInt>();
        _exportButton = root.Q<Button>("export-button");
        _createTexButton = root.Q<Button>("create-tex-button");
        _widthField = root.Q<IntegerField>("width-field");
        _heightField = root.Q<IntegerField>("height-field");
        _tint = root.Q<ColorField>("tint");
        _alphaInput = root.Q<IntegerField>("alpha-input");

        _textureField.RegisterValueChangedCallback<Object>(TextureSelected);
        _alphaDropDown.RegisterValueChangedCallback<string>(AlphaOptionSelected);
        _textureOption.RegisterValueChangedCallback<string>(TextureOptionSelected);
        _alphaSlider.RegisterValueChangedCallback<int>(AlphaSliderChanged);
        _alphaInput.RegisterValueChangedCallback<int>(AlphaInputChanged);
        _alphaGradient.RegisterValueChangedCallback<Gradient>(AlphaGradientChanged);
        _tint.RegisterValueChangedCallback<Color>(TintChanged);
        
        _exportButton.clicked += ()=> ExportImage(_outputTexture);
        _createTexButton.clicked += CreateTexture;

        _imagePreview.style.backgroundImage = null;
        TextureOptionSelected(null);
        AlphaOptionSelected(null);
        
    }

    private void CreateTexture()
    {
        var textureWidth = _widthField.value;
        var textureHeight = _heightField.value;
        _selectedTexture = new Texture2D(textureWidth , textureHeight , TextureFormat.RGBA32 ,false);
        for(var i = 0; i < textureWidth ; i++)
        {
            for(var j = 0 ; j <textureHeight ; j++)
            {
                _selectedTexture.SetPixel(i , j , Color.white);
            }
        }

        _selectedTexture.Apply();

        var greaterWidth = (textureWidth > textureHeight);
        var xRatio = 1f;
        var yRatio = 1f;
        if(greaterWidth)
        {
            yRatio = (float) textureHeight / (float) textureWidth;
        }
        else
        {
            xRatio = (float) textureWidth / (float) textureHeight;
        }

        _imagePreview.style.width = 300f * xRatio;
        _imagePreview.style.height = 300f * yRatio;

        ApplyAlphaGradient();

    }

    private void ExportImage(Texture2D outputTexture)
    {
        
    }

    private void TintChanged(ChangeEvent<Color> evt)
    {
       
    }

    private void AlphaGradientChanged(ChangeEvent<Gradient> evt)
    {
        
    }

    private void AlphaInputChanged(ChangeEvent<int> evt)
    {
        
    }

    private void AlphaSliderChanged(ChangeEvent<int> evt)
    {
        
    }

    private void TextureOptionSelected(ChangeEvent<string> evt)
    {
        ApplyAlphaGradient();
    }

    private void AlphaOptionSelected(ChangeEvent<string> evt)
    {
        ApplyAlphaGradient();
    }

    private void TextureSelected(ChangeEvent<Object> evt)
    {
        throw new System.NotImplementedException();
    }

    private void ApplyAlphaGradient()
    {
        if(_selectedTexture == null)
        {
            _exportButton.SetEnabled(false);
            return;
        }
        _exportButton.SetEnabled(true);
        _outputTexture = _selectedTexture;
        _imagePreview.style.backgroundImage = _outputTexture;

    }
}

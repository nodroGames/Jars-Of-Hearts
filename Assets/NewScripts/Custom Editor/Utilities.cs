//using UnityEngine;
//using UnityEditor;

//public static class Utilities
//{
// public class LineAttribute : PropertyAttribute
//    {
//        public float paddingAbove;
//        public float paddingUnder;
//        public float thickness;
//        public Color color;

//        public LineAttribute(float _thickness = 1f, float _paddingAbove = 5f, float _paddingUnder = 5f, float _r = 0f, float _g = 0f, float _b = 0f, float _a = 1f)
//        {
//            paddingAbove = _paddingAbove;
//            paddingUnder = _paddingUnder;
//            thickness = _thickness;
//            color = new Color(_r, _g, _b, _a);
//        }
//    }

//    [CustomPropertyDrawer(typeof(LineAttribute))]
//    public class LineDrawer : PropertyDrawer
//    {
//        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//        {
//            LineAttribute a = attribute as LineAttribute;

//            return base.GetPropertyHeight(property, label) + a.paddingAbove + a.thickness + a.paddingUnder;
//        }

//        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//        {
//            LineAttribute a = attribute as LineAttribute;

//            position.y += a.paddingAbove;
//            position.height = a.thickness;

//            EditorGUI.DrawRect(position, a.color);

//            float ph = base.GetPropertyHeight(property, label);

//            position.y += position.height + a.paddingUnder;
//            position.height = ph;

//            EditorGUI.PropertyField(position, property, label, true);
//        }
//    }
//}
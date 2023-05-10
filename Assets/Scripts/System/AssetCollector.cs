using UnityEngine;
using Melanchall.DryWetMidi;
using Melanchall.DryWetMidi.Core;

// ��ũ��Ʈ���� ���� ���۷��� �� �� �ְ� ���ִ� ������?, �ڿ� ����� �߰�
[CreateAssetMenu(fileName ="AssetCollector")]
public class AssetCollector : ScriptableObject
{
    [Header("HP�� UI")]
    public GameObject hpUI;
    public GameObject hpCounterUI;
    public Sprite hpCounterSpriteFull;
    public Sprite hpCounterSpriteEmpty;
}

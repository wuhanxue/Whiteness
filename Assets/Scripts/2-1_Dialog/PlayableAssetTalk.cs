using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

[System.Serializable]
public class PlayableAssetTalk : PlayableAsset
{
	public ExposedReference<UILabel> talkText;

	public string talkStr;

	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
		PlayableTrackTalk talkPlayable = new PlayableTrackTalk();

		talkPlayable.talkText1 = talkText.Resolve(graph.GetResolver());

		talkPlayable.talkStr1 = talkStr;
		return ScriptPlayable<PlayableTrackTalk>.Create(graph, talkPlayable);
	}
}

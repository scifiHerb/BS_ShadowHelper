# BS_ShadowHelper
![Beat Saber 2023 10 01 - 11 33 35 01 mp4_snapshot_04 25 953](https://github.com/scifiHerb/BS_ShadowHelper/assets/109839172/a0444570-22f8-402d-8d86-aeedf6cec9f9)
![Beat Saber 2023 10 01 - 11 33 35 01 mp4_snapshot_04 46 053](https://github.com/scifiHerb/BS_ShadowHelper/assets/109839172/f4b5ce70-85fc-4e6a-bf99-010a1f2fdc7d)
# 説明
ビートセイバーに影を追加するMODです。
Modsettingsで影の方向や光の強さ、影の解像度などの変更ができます。  
現状では対応アバターはなるるるるなさんの[NalulunaAvatars](https://nalulululuna.fanbox.cc/posts/2079749)のみです、支援者限定ですが確かフリー版もあるのでそちらでも問題なく動くと思います。  
Bloom使用時は付属のテスト用プラットフォーム及びカスタムアバター用の置き換えシェーダーでは光ってしまいます、影とBloom対応のシェーダー考え中です。  

*まだまだ追加したい機能沢山ありますが現状技術的に作れる範囲で最低限実装しました、使ってもらえると嬉しいです！    
*また動作確認用にプラットフォームつけてみました。

# 直したいところ、不具合  
・アバターはメニューシーン、ゲームシーンの読み込み時に取得して各種変更をしているので初回起動時は一度ModSettingsでOKを押すかゲームシーンに入る必要がある。  
・Bloom使用時
・ゲームシーンに入った時光が描画されないことがありました（ModSettingsでOK押し直したら直りました)

# 設定項目
{  
  "enable": true,  MODのオンオフ、設定後再起動する必要があります  
  "direLightRotationX": 30.0,  各種DirectionalLightの角度  
  "direLightRotationY": 0.0,  
  "direLightRotationZ": 0.0,  
  "intensity": 1.0,  光の明るさです、一定以下の数値になると、影の描画がされなくなるようです  
  "shadowResolution": 10000,  影の解像度です、割と高い数値にしても大してFPSに影響ありませんでした  
  "lightShadows": "Hard",  (Hard / Soft)はっきりした影か柔らかな影かですHardのほうが軽いそうです  
  "overrideCustomAvatarMaterials": true  カスタムアバターのアバターはデフォルトのシェーダーだと影でない様なのでその場合シェーダーを置き換えるかどうかです、具体的には"BeatSaber/Unlit Glow"を置き換えてます。  
}

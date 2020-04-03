using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour {

	// 攻击面板按钮
	UIButton attackBtn;
	// 魔法面板按钮
	UIButton magicBtn;
	// 战技面板按钮
	UIButton tacticsBtn;
	// 道具面板按钮
	UIButton itemBtn;
	// 连击面板按钮
	UIButton comboBtn;
	// 撤退面板按钮
	UIButton retreatBtn;
	// 面板列表
	List<GameObject> panelList = new List<GameObject>();
	// 菜单面板
	GameObject menuPanel;
	// 攻击面板
	GameObject attackPanel;
	// 魔法面板
	GameObject magicPanel;
	// 战技面板
	GameObject tacticsPanel;
	// 道具面板
	GameObject itemPanel;
	// 连击面板
	GameObject comboPanel;
	// 撤退面板
	GameObject retreatPanel;
	// 标识玩家是否已经选择
	public bool isChoosed { get; set; }

	void Awake()
	{
		attackBtn = GameObject.Find(Const.Btn_1).GetComponent<UIButton>();
		magicBtn = GameObject.Find(Const.Btn_2).GetComponent<UIButton>();
		tacticsBtn = GameObject.Find(Const.Btn_3).GetComponent<UIButton>();
		itemBtn = GameObject.Find(Const.Btn_4).GetComponent<UIButton>();
		comboBtn = GameObject.Find(Const.Btn_5).GetComponent<UIButton>();
		retreatBtn = GameObject.Find(Const.Btn_6).GetComponent<UIButton>();
		attackBtn.onClick.Add(new EventDelegate(() => { PlayerMenuChoose(Const.Panel_001); }));
		magicBtn.onClick.Add(new EventDelegate(() => { PlayerMenuChoose(Const.Panel_002); }));
		tacticsBtn.onClick.Add(new EventDelegate(() => { PlayerMenuChoose(Const.Panel_003); }));
		itemBtn.onClick.Add(new EventDelegate(() => { PlayerMenuChoose(Const.Panel_004); }));
		comboBtn.onClick.Add(new EventDelegate(() => { PlayerMenuChoose(Const.Panel_005); }));
		retreatBtn.onClick.Add(new EventDelegate(() => { PlayerMenuChoose(Const.Panel_006); }));
		menuPanel = gameObject;
		attackPanel = GameObject.Find(Const.AttackPanel);
		magicPanel = GameObject.Find(Const.MagicPanel);
		tacticsPanel = GameObject.Find(Const.TacticsPanel);
		itemPanel = GameObject.Find(Const.ItemPanel);
		comboPanel = GameObject.Find(Const.ComboPanel);
		retreatPanel = GameObject.Find(Const.RetreatPanel);
		// 加入列表
		panelList.Add(menuPanel);
		panelList.Add(attackPanel);
		panelList.Add(magicPanel);
		panelList.Add(tacticsPanel);
		panelList.Add(itemPanel);
		panelList.Add(comboPanel);
		panelList.Add(retreatPanel);
	}

	void Start()
	{
		// 启动时关闭所有面板
		CloseAllPanel();
	}

	void PlayerMenuChoose(string panelId)
	{
		Debug.Log("Choose " + panelId);
		isChoosed = true;
		// 玩家操作，打开面板
		OpenTargetPanel(panelId);
	}

	/// <summary>
	/// 打开特定面板
	/// </summary>
	/// <param name="panelId"></param>
	public void OpenTargetPanel(string panelId)
	{
		switch (panelId)
		{
			// 菜单面板
			case Const.Panel_000:
				if (!menuPanel.activeSelf && !isChoosed)
				{
					CloseAllPanel();
					menuPanel.SetActive(true);
				}
				break;
			// 攻击面板
			case Const.Panel_001:
				if (!attackPanel.activeSelf)
				{
					CloseAllPanel();
					attackPanel.SetActive(true);
				}
				break;
			// 魔法面板
			case Const.Panel_002:
				if (!magicPanel.activeSelf)
				{
					CloseAllPanel();
					magicPanel.SetActive(true);
				}
				break;
			// 战技面板
			case Const.Panel_003:
				if (!tacticsPanel.activeSelf)
				{
					CloseAllPanel();
					tacticsPanel.SetActive(true);
				}
				break;
			// 道具面板
			case Const.Panel_004:
				if (!itemPanel.activeSelf)
				{
					CloseAllPanel();
					itemPanel.SetActive(true);
				}
				break;
			// 连击面板
			case Const.Panel_005:
				if (!comboPanel.activeSelf)
				{
					CloseAllPanel();
					comboPanel.SetActive(true);
				}
				break;
			// 撤退面板
			case Const.Panel_006:
				if (!retreatPanel.activeSelf)
				{
					CloseAllPanel();
					retreatPanel.SetActive(true);
				}
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// 关闭所有面板
	/// </summary>
	public void CloseAllPanel()
	{
		panelList.ForEach(p => p.SetActive(false));
	}

	
}

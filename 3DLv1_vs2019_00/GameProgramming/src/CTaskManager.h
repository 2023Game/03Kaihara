#ifndef CTASKMANAGER_H
#define CTASKMANAGER_H
//タスククラスのインクルード
#include "CTask.h"

/*
タスクマネージャ
タスクリストの管理
*/
class CTaskManager {
public:
	//衝突処理
	void CTaskManager::Collision()
	{
		//先頭から最後まで繰り返し
		CTask* task = mHead.mpNext;
		while (task->mpNext) {
			//衝突処理を呼ぶ
			task->Collision();
			//次へ
			task = task->mpNext;
		}
	}

	//インスタンスの取得
	static CTaskManager* Instance();
	//タスクの削除
	void Delete();
	//リストから削除
	//Remove(タスクのポインタ)
	void Remove(CTask* task);
	//デストラクタ
	virtual ~CTaskManager();
	//リストに追加
	//Add(タスクのポインタ)
	void Add(CTask* addTask);
	//更新
	void Update();
	//描画
	void Render();
private:
	//タスクマネージャのインスタンス
	static CTaskManager* mpInstance;
protected:
	//デフォルトコンストラクタ
	CTaskManager();
	CTask mHead;//先頭タスク
	CTask mTail;//最終タスク
};

#endif


import { SuitTypeView } from '../enums-views/suit-type-view';
import { RankTypeView } from '../enums-views/rank-type-view';

export class GetUnfinishedGameResponseView
{
    public user: UserGetUnfinishedGameResponseView;
    public bots: Array<BotGetUnfinishedGameResponseViewItem>;
    public winner: string;
  }
  
  export class UserGetUnfinishedGameResponseView
  {
    public name:string;
    public points: number;
    public cards: Array<StepUserGetUnfinishedGameResponseViewItem>;

  }

  export class StepUserGetUnfinishedGameResponseViewItem
  {
    public suit: SuitTypeView;
    public rank: RankTypeView;
  }

  export class BotGetUnfinishedGameResponseViewItem
  {
    public name:string;
    public points: number;
    public cards: Array<StepBotGetUnfinishedGameResponseViewItem>;
  }

  export class StepBotGetUnfinishedGameResponseViewItem
  {
    public suit: SuitTypeView;
    public rank: RankTypeView;
  }

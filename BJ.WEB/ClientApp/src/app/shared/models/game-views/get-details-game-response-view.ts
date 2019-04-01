import { SuitTypeView } from '../enums-views/suit-type-view';
import { RankTypeView } from '../enums-views/rank-type-view';

export class GetDetailsGameResponseView
{
    public user: UserGetDetailsGameResponseView;
    public bots: Array<BotGetDetailsGameResponseViewItem>;
    public winner: string;
  }
  
  export class UserGetDetailsGameResponseView
  {
    public name:string;
    public points: number;
    public cards: Array<StepUserGetDetailsGameResponseViewItem>;

  }

  export class StepUserGetDetailsGameResponseViewItem
  {
    public suit: SuitTypeView;
    public rank: RankTypeView;
  }

  export class BotGetDetailsGameResponseViewItem
  {
    public name:string;
    public points: number;
    public cards: Array<StepBotGetDetailsGameResponseViewItem>;
  }

  export class StepBotGetDetailsGameResponseViewItem
  {
    public suit: SuitTypeView;
    public rank: RankTypeView;
  }
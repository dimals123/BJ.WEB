import { SuitTypeView } from '../enums-views/suit-type-view';
import { RankTypeView } from '../enums-views/rank-type-view';

export class GetUnfinishedGameResponseView
{
    public user: UserGetUnfinishedGameResponseView;
    public bots: Array<BotGetUnfinishedGameResponseViewItem>;
    public winner: string;
/**
 *
 */
    constructor() {
      this.bots = []; 
    } 
  }
  
  export class UserGetUnfinishedGameResponseView
  {
    public name:string;
    public points: number;
    public cards: Array<StepUserGetUnfinishedGameResponseViewItem>;

    constructor() {
      this.cards = []; 
    } 

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
    
    constructor() {
      this.cards = []; 
    } 
  }

  export class StepBotGetUnfinishedGameResponseViewItem
  {
    public suit: SuitTypeView;
    public rank: RankTypeView;
  }

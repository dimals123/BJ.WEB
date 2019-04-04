import { SuitTypeView } from '../enums-views/suit-type-view';
import { RankTypeView } from '../enums-views/rank-type-view';

export class GetDetailsGameResponseView
{
    public user: UserGetDetailsGameResponseView;
    public bots: Array<BotGetDetailsGameResponseViewItem>;
    public winner: string;
    /**
     *
     */
    constructor() {
      this.bots = [];
    }
  }
  
  export class UserGetDetailsGameResponseView
  {
    public name:string;
    public points: number;
    public cards: Array<StepUserGetDetailsGameResponseViewItem>;
    /**
     *
     */
    constructor() {
      this.cards = [];
    }

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
    /**
     *
     */
    constructor() {
     this.cards = [];
    }
  }

  export class StepBotGetDetailsGameResponseViewItem
  {
    public suit: SuitTypeView;
    public rank: RankTypeView;
  }
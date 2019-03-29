export class GetUnfinishedGameResponseView
{
    user: UserGetUnfinishedGameResponseView;
    bots: Array<BotGetUnfinishedGameResponseViewItem>;
    winner: string;
  }
  
  export class UserGetUnfinishedGameResponseView
  {
      name:string;
      points: number;
      cards: Array<StepUserGetUnfinishedGameResponseViewItem>;

  }

  export class StepUserGetUnfinishedGameResponseViewItem
  {
      suit: number;
      rank: number;
  }

  export class BotGetUnfinishedGameResponseViewItem
  {
    name:string;
    points: number;
    cards: Array<StepBotGetUnfinishedGameResponseViewItem>;
  }

  export class StepBotGetUnfinishedGameResponseViewItem
  {
      suit: number;
      rank: number;
  }
export class StartGameResponseView
{
    user: UserStartGameResponseView;
    bots: Array<BotStartGameResponseViewItem>;
    winner: string;
  }
  
  export class UserStartGameResponseView
  {
      name:string;
      points: number;
      cards: Array<StepUserStartGameResponseViewItem>;

  }

  export class StepUserStartGameResponseViewItem
  {
      suit: number;
      rank: number;
  }

  export class BotStartGameResponseViewItem
  {
    name:string;
    points: number;
    cards: Array<StepBotStartGameResponseViewItem>;
  }

  export class StepBotStartGameResponseViewItem
  {
      suit: number;
      rank: number;
  }
import {PlansCardModel} from "../../_models/plansCardModel";

export class InvestmentList {
  static plansCard: PlansCardModel[] = [
    {
      name: "IslamicStarterPlan",
      displayName: 'Islamic Starter Plan',
      return: "Weekly 5-7.5 %",
      every: " week",
      duration: "4 week",
      min: 10,
      max: 200,
      total: "Up to 30%",
      totalDisplay: "20-30 %"
    },
    {
      name: "IslamicSilverPlan",
      displayName: 'Islamic Silver Plan',
      return: "Weekly 7.5-10 %",
      every: " week",
      duration: "16 week",
      min: 75,
      max: 749,
      total: "Up to 160%",
      totalDisplay: "120-160 %"
    },
    {
      name: "IslamicGoldenPlan",
      displayName: 'Islamic Golden Plan',
      return: "Weekly 10-12.5 %",
      every: " week",
      duration: "18 week",
      min: 750,
      max: 1999,
      total: "Up to 225%",
      totalDisplay: "180-225 %"
    },
    {
      name: "IslamicDiamondPlan",
      displayName: 'Islamic Diamond Plan',
      return: "Weekly 12.5-15 %",
      every: " week",
      duration: "20 week",
      min: 2000,
      max: 4999,
      total: "Up to 300%",
      totalDisplay: "250-300% "
    },
    {
      name: "IslamicPearlPlan",
      displayName: 'Islamic Pearl Plan',
      return: "Weekly 15-17.5 %",
      every: " week",
      duration: "24 week",
      min: 5000,
      max: 14999,
      total: "Up to 420%",
      totalDisplay: "360-420% "
    },
    {
      name: "IslamicPlatinumPlan",
      displayName: 'Islamic Platinum Plan',
      return: "Weekly 17.5-20 %",
      every: " week",
      duration: "28 week",
      min: 15000,
      max: 0,
      total: "Up to 560%",
      totalDisplay: "360-420% "
    },
    {
      name: "IslamicMindsTradePlan",
      displayName: 'Islamic MindsTrade Plan',
      return: "Every 30 day",
      every: " After 30 day",
      duration: "120 days",
      min: 100,
      max: 0,
      total: "50%",
      totalDisplay: "50% Of the total profit "
    }
  ]

  static getInvestmentList() {
    return this.plansCard;

  }
}

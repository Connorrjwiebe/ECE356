import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { BasicAccordion } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <BasicAccordion />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  }
];

export default AppRoutes;

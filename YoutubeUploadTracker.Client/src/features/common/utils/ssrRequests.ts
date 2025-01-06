import { backendUrl } from "./urls";
import { PageContext } from "vike/types";

type FetchParams = Parameters<typeof fetch>;

export const httpGet = async <T>(
  pageContext: PageContext,
  url: FetchParams[0],
  options?: FetchParams[1]
): Promise<T | undefined> => {
  const cookies = pageContext.headers!.cookie;

  const fetchOptions: FetchParams[1] = {
    ...options,
    headers: {
      ...options?.headers,
      Cookie: cookies,
    },
  };

  const response = await fetch(`${backendUrl}${url}`, fetchOptions);

  if (response.status !== 200) {
    return undefined;
  }

  return await response.json();
};

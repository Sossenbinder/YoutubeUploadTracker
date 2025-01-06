import { backendUrl } from "./urls";

type FetchParams = Parameters<typeof fetch>;

export const httpGet = async <T>(
  url: FetchParams[0],
  options?: FetchParams[1]
): Promise<T | undefined> => {
  const response = await fetch(`${backendUrl}${url}`, options);

  if (response.status !== 200) {
    return undefined;
  }

  return await response.json();
};

import { ref } from 'vue';

const isLoadingRef = ref(false);

export function useLoading() {
  const setLoading = (value: boolean) => {
    isLoadingRef.value = value;
  };
  return { isLoading: isLoadingRef, setLoading };
}
